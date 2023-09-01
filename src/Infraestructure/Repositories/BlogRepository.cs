using MongoDB.Driver;
using System;
using Core;
using Core.Entities;
using System.Collections.Generic;
using Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IMongoCollection<Blog> _blog;

        public BlogRepository(IWebsiteDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _blog = database.GetCollection<Blog>(settings.BlogCollectionName);
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            var blogs = _blog.Find(_ => true);
            return await blogs.ToListAsync();
        }
    }
}
