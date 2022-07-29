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
    public class ContactRepository : IContactRepository
    {
        private readonly IMongoCollection<Contact> _contact;

        public ContactRepository(IWebsiteDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _contact = database.GetCollection<Contact>(settings.ContactCollectionName);
        }

        public async Task<Contact> InsertAsync(Contact contact)
        {
            contact.InsertDate = DateTime.Now;

            await _contact.InsertOneAsync(contact);

            return contact;
        }

        public async Task<Contact> ReplaceAsync(Contact contact)
        {
            var filter = new BsonDocument("Id", "Jack");
            var update = Builders<Contact>.Update.Set("FirstName", "John");

            var result = _contact.FindOneAndUpdate(filter, update);
            //await _contact.UpdateOneAsync(contact);

            return contact;
        }

        public async Task<long> FindNotReadLastMonthAsync(Contact contact)
        {
            var builder = Builders<Contact>.Filter;

            var filter = builder.Eq(x => x.Email, contact.Email)
                & builder.Eq(x => x.IsRead, false)
                & builder.Gte(x => x.InsertDate, new BsonDateTime(DateTime.Today.AddMonths(-1)));

            var count = await _contact.CountDocumentsAsync(filter);

            return count;
        }
    }
}
