using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Entities
{
    public class Contact : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReadDate { get; set; }

        public bool IsValidEmail()
        {
            var emailAddressAtribute = new EmailAddressAttribute();
            return emailAddressAtribute.IsValid(Email);
        }
    }
}
