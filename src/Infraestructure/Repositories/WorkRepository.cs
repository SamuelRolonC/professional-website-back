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

        public List<Work> Get() =>
           _work.Find(work => true).ToList();

        public async Task<List<Work>> GetAsync() =>
           await _work.Find(work => true).ToListAsync();

        public Work Get(string id) =>
            _work.Find(work => work.Id == id).FirstOrDefault();

        public async Task<Work> GetAsync(string id) =>
            await _work.Find(work => work.Id == id).FirstOrDefaultAsync();

        public Work Create(Work work)
        {
            _work.InsertOne(work);
            return work;
        }

        public async Task<Work> CreateAsync(Work work)
        {
            await _work.InsertOneAsync(work);
            return work;
        }

        public void Update(string id, Work workIn) =>
            _work.ReplaceOne(work => work.Id == id, workIn);

        public async void UpdateAsync(string id, Work workIn) =>
            await _work.ReplaceOneAsync(work => work.Id == id, workIn);

        public void Remove(Work workIn) =>
            _work.DeleteOne(work => work.Id == workIn.Id);

        public async void RemoveAsync(Work workIn) =>
            await _work.DeleteOneAsync(work => work.Id == workIn.Id);

        public void Remove(string id) =>
            _work.DeleteOne(work => work.Id == id);

        public async void RemoveAsync(string id) =>
            await _work.DeleteOneAsync(work => work.Id == id);
    }
}
