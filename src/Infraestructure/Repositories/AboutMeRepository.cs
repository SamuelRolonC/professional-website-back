using MongoDB.Driver;
using System;
using Core;
using Core.Entities;
using System.Collections.Generic;
using Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class AboutMeRepository : IAboutMeRepository
    {
        private readonly IMongoCollection<AboutMe> _aboutMe;

        public AboutMeRepository(IWebsiteDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _aboutMe = database.GetCollection<AboutMe>(settings.AboutMeCollectionName);
        }

        public Task<AboutMe> GetByLanguageAsync(string language)
        {
            var aboutMe = _aboutMe.Find(x => x.Language == language);
            return aboutMe.FirstOrDefaultAsync();
        }
    }
}
