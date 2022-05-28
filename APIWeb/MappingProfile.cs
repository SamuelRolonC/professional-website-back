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
                .ForMember(vm => vm.CompanyViewModel, opt => opt.MapFrom(e => e.Company));

            CreateMap<Company, CompanyViewModel>();
        }
    }
}
