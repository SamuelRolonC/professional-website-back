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
                .ForMember(d => d.CompanyViewModel, x => x.MapFrom(s => s.Company));

            CreateMap<Company, CompanyViewModel>();

            CreateMap<ProfessionalDataReport, EntirePageViewModel>()
                .ForMember(d => d.AboutMeViewModel, x => x.MapFrom(s => s.AboutMe))
                .ForMember(d => d.ListWorkViewModel, x => x.MapFrom(s => s.WorkList))
                //.ForMember(d => d.ListWorkViewModel, 
                    //x => x.MapFrom(s => GenerateListWorkViewModel(e.WorkList)))
                .ForMember(d => d.ListProjectViewModel, x => x.MapFrom(s => s.ProjectList));

            CreateMap<AboutMe, AboutMeViewModel>();

            CreateMap<Work, WorkViewModel>()
                .ForMember(d => d.CompanyViewModel, x => x.MapFrom(s => s.Company));

            CreateMap<Project, ProjectViewModel>();

            CreateMap<EmailFormModel, Contact>()
                .ForMember(d => d.Email, x => x.MapFrom(s => s.Address));
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
