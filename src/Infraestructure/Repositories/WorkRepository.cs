using MongoDB.Driver;
using System;
using Core;
using Core.Entities;
using System.Collections.Generic;
using Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private readonly IMongoCollection<Work> _work;

        public WorkRepository(IWebsiteDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _work = database.GetCollection<Work>(settings.WorkCollectionName);
        }

        public async Task<List<Work>> GetByLanguageAsync(string language)
        {
            var builder = Builders<Work>.Filter;
            var filter = builder.Eq("language", language);
            var cursor = await _work.FindAsync(filter);

            return cursor.ToList();
        }
    }
}
