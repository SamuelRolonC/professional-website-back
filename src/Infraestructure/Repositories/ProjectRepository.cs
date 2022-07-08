using MongoDB.Driver;
using System;
using Core;
using Core.Entities;
using System.Collections.Generic;
using Core.Interfaces.Repositories;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Infraestructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _project;

        public ProjectRepository(IWebsiteDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _project = database.GetCollection<Project>(settings.ProjectCollectionName);
        }

        public async Task<List<Project>> GetByLanguageAsync(string language) 
        {
            var builder = Builders<Project>.Filter;
            var filter = builder.Eq("language", language);
            var cursor = await _project.FindAsync(filter);
            
            return cursor.ToList();
        }
    }
}
