$configFile = "db-scripts-runner.conf";

Write-Host "Reading config file...";
if (!(Test-Path -Path $configFile -PathType Leaf)) {
    Write-Host -ForegroundColor Red "Configuration file $configFile not found!";
    return;
}
Get-Content $configFile | ForEach-Object -begin {$h=@{}} -process { $k = [regex]::split($_,'='); if(($k[0].CompareTo("") -ne 0) -and ($k[0].StartsWith("[") -ne $True)) { $h.Add($k[0], $k[1]) } }

# Constants
$dbScriptsPath = $h.DbScriptsPath;
$mongoHost = $h.Host;
$mongoPort = $h.Port;
$version = $h.Version
$runScriptFileNameTemplate = "00000000-000000-run-v-*.js";
$scriptsRunnerFilename = "$dbScriptsPath\"+$runScriptFileNameTemplate.Replace("*",$version);

Write-Host "Listing script files...";
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