using System;
using System.Collections.Generic;

namespace APIWeb
{
    public class EntirePageViewModel
    {
        public AboutMeViewModel AboutMeViewModel { get; set; }
        public IEnumerable<WorkViewModel> ListWorkViewModel { get; set; }
        public IEnumerable<ProjectViewModel> ListProjectViewModel { get; set; }
        public string ErrorMessage { get; set; }
    }
}
