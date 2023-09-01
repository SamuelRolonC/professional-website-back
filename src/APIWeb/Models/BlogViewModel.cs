using System;
using System.Collections.Generic;

namespace APIWeb
{
    public class BlogViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        public string Url { get; set; }
        public bool IsPinned { get; set; }
    }
}
