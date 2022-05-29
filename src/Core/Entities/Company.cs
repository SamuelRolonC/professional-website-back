using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Company : IEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        public string Url { get; set; }

        public string Location { get; set; }
    }
}
