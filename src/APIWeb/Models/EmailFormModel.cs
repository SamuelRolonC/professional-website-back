using System;
using System.Text.Json.Serialization;

namespace APIWeb
{
    public class EmailFormModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
