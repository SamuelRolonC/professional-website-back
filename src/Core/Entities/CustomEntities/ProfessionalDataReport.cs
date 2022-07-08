using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ProfessionalDataReport : IEntity
    {
        public AboutMe AboutMe { get; set; }
        public List<Project> ProjectList { get; set; }
        public List<Work> WorkList { get; set; }
    }
}
