﻿namespace Core
{
    public class WebsiteDatabaseSettings : IWebsiteDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        #region Collections

        public string WorkCollectionName { get; set; }
        public string AboutMeCollectionName { get; set; }
        public string ProjectCollectionName { get; set; }
        public string ContactCollectionName { get; set; }
        public string BlogCollectionName { get; set; }

        #endregion
    }

    public interface IWebsiteDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

        #region Collections

        string WorkCollectionName { get; set; }
        string AboutMeCollectionName { get; set; }
        string ProjectCollectionName { get; set; }
        string ContactCollectionName { get; set; }
        string BlogCollectionName { get; set; }

        #endregion
    }
}
