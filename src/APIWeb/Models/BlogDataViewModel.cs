using System;
using System.Collections.Generic;

namespace APIWeb
{
    public class BlogDataViewModel
    {
        public IEnumerable<BlogViewModel> ListBlogViewModel { get; set; }
        public string ErrorMessage { get; set; }
    }
}
