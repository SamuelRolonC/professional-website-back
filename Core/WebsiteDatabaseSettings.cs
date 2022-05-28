namespace Core
{
    public class WebsiteDatabaseSettings : IWebsiteDatabaseSettings
    {
        public string WorkCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IWebsiteDatabaseSettings
    {
        string WorkCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
