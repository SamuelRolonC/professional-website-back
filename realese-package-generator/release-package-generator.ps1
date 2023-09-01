$releasePackageGeneratorConfFilename = "release-package-generator.conf";

Write-Host "Reading config file...";
if (!(Test-Path -Path $releasePackageGeneratorConfFilename -PathType Leaf)) {
    Write-Host -ForegroundColor Red "Configuration file $releasePackageGeneratorConfFilename not found!";
    return;
}
Get-Content $releasePackageGeneratorConfFilename | ForEach-Object -begin {$conf=@{}} -process { $k = [regex]::split($_,'='); if(($k[0].CompareTo("") -ne 0) -and ($k[0].StartsWith("[") -ne $True)) { $conf.Add($k[0], $k[1]) } }

# Constants
$packagesFolderPath = $conf.PackagesFolderPath;
$packageFolderNameTemplate = $conf.PackageFolderNameTemplate;
$version = $conf.Version

$packageFolderName = $packageFolderNameTemplate.Replace("*",$version);
$packageFolderPath = "$packagesFolderPath\$packageFolderName";
$supportFolderName = "support";
$supportFolderOriginPath = $supportFolderName;
$supportFolderDestinationPath = "$packageFolderPath\$supportFolderName";
$dbScriptsRunnerConfigFilename = "db-scripts-runner.conf";
$dbScriptsRunnerDestinationConfigPath = "$supportFolderDestinationPath\$dbScriptsRunnerConfigFilename";
$dbNextReleaseFolderPath = "..\db\next-release";
$dbReleaseFolderPath = "..\db\releases\"+$packageFolderName;
$dbPackageFolderPath = $packageFolderPath+"\db-scripts";

if (!(Test-Path -Path $supportFolderOriginPath)) {
    Write-Host -ForegroundColor Red "Support folder $supportFolderOriginPath not found!";
    return;
}

# TODO Check if exists and if it does, ask for clearing that folder.

if (!(Test-Path -Path $packagesFolderPath)) {
    Write-Host -ForegroundColor Red "Packages folder $packagesFolderPath not found!";
    return;
}

if (Test-Path -Path $packageFolderPath) {
    $confirmation = Read-Host "The package folder $packageFolderPath already exists. If continues, the folder will be cleared. You want to continue? [y/n]";

    while ($confirmation -ne 'y' -and $confirmation -ne 'n') {
        $confirmation = Read-Host "Enter 'y' or 'n'";
    }

    if ($confirmation -eq 'n') {
        Write-Host "Program finished without generating release package.";
        exit;
    }

    # TODO Clear-Content $packageFolderPath;
}

Write-Host "Creating package folder...";
New-Item -Path $packagesFolderPath -Name $packageFolderName -ItemType "directory" | Out-Null


Write-Host "Generating support folder...";
Copy-Item -Path $supportFolderOriginPath -Destination $supportFolderDestinationPath -Recurse

# Declares function to edit parameters of config file.
function updateConfig($configFilePath, $attribute, $value) { 
    (Get-Content $configFilePath) |
        Foreach-Object { $_ -replace "$attribute=.*", "$attribute=$value" } |
        Set-Content $configFilePath;
}

# Sets the Version parameter in db-scripts-runner.conf
updateConfig $dbScriptsRunnerDestinationConfigPath "Version" $version;

Write-Host "dbNextReleaseFolderPath $dbNextReleaseFolderPath\*";
Write-Host "dbReleaseFolderPath $dbReleaseFolderPath";

Write-Host "Generating database scripts folder in repository...";
Copy-Item -Path $dbNextReleaseFolderPath -Destination $dbReleaseFolderPath -Recurse

Write-Host "Generating database scripts folder in release package...";
Copy-Item -Path $dbNextReleaseFolderPath -Destination $dbPackageFolderPath -Recurse

return;

$listScriptsFileName = 
    Get-ChildItem -Path "$dbScriptsPath\*" -Include *.js -Exclude $runScriptFileNameTemplate| 
    Select-Object Name, FullName;

if (Test-Path -Path $scriptsRunnerFilename -PathType Leaf) {
    Write-Host "Clearing existent runner file...";
    Clear-Content $scriptsRunnerFilename;
}

Write-Host "Generating $scriptsRunnerFilename...";
$dateTime = Get-Date -Format "dd/MM/yyyy HH:mm:ss";
"// Generated at $dateTime" | Out-File $scriptsRunnerFilename -Encoding utf8 -Append
"" | Out-File $scriptsRunnerFilename -Encoding utf8 -Append

$listScriptsFileName | ForEach-Object {
    $fullName = $_.FullName.Replace("\", "\\");
    "console.log('"+$_.Name+"');" | Out-File $scriptsRunnerFilename -Encoding utf8 -Append
    "load('$fullName');" | Out-File $scriptsRunnerFilename -Encoding utf8 -Append
}

Write-Host "Executing script runner...";
mongosh --host $mongoHost --port $mongoPort --file $scriptsRunnerFilename

Write-Host "Script runner finished.";