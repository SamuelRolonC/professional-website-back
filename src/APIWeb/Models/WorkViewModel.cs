using System;
using System.Text.Json.Serialization;

namespace APIWeb
{
    public class WorkViewModel
    {
        public string Id { get; set; }
        [JsonPropertyName("company")]
        public CompanyViewModel CompanyViewModel { get; set; }

        public string Position { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrentJob { get; set; }

        public string Type { get; set; }
    }
}
