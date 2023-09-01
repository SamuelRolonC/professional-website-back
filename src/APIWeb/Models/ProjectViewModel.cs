using System;
using System.Text.Json.Serialization;

namespace APIWeb
{
    public class ProjectViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string Language { get; set; }
    }
}
