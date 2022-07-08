using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Work, WorkViewModel>()
                .ForMember(vm => vm.CompanyViewModel, x => x.MapFrom(e => e.Company));

            CreateMap<Company, CompanyViewModel>();

            CreateMap<ProfessionalDataReport, EntirePageViewModel>()
                .ForMember(vm => vm.AboutMeViewModel, x => x.MapFrom(e => e.AboutMe))
                .ForMember(vm => vm.ListWorkViewModel, x => x.MapFrom(e => e.WorkList))
                //.ForMember(vm => vm.ListWorkViewModel, 
                    //x => x.MapFrom(e => GenerateListWorkViewModel(e.WorkList)))
                .ForMember(vm => vm.ListProjectViewModel, x => x.MapFrom(e => e.ProjectList));

            CreateMap<AboutMe, AboutMeViewModel>();

            CreateMap<Work, WorkViewModel>()
                .ForMember(vm => vm.CompanyViewModel, x => x.MapFrom(e => e.Company));

            CreateMap<Project, ProjectViewModel>();
        }

        private List<WorkViewModel> GenerateListWorkViewModel(List<Work> workList)
        {
            var list = new List<WorkViewModel>();

            workList.ForEach(x => list.Add(new WorkViewModel()
            {
                Id = x.Id,
                CompanyViewModel = new CompanyViewModel()
                {
                    Name = x.Company.Name,
                    Location = x.Company.Location,
                    Url = x.Company.Url
                },
                Position = x.Position,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsCurrentJob = x.IsCurrentJob,
                Type = x.Type
            }));

            return list;
        }
    }
}
