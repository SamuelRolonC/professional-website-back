using Core.Entities;
using Core.Interfaces.Services;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class ProfessionalDataService : IProfessionalDataService
    {
        private readonly WorkRepository _workRepository;
        private readonly AboutMeRepository _aboutMeRepository;
        private readonly ProjectRepository _projectRepository;

        public ProfessionalDataService(WorkRepository workRepository
            , AboutMeRepository aboutMeRepository
            , ProjectRepository projectRepository)
        {
            _workRepository = workRepository;
            _aboutMeRepository = aboutMeRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ProfessionalDataReport> GetByLanguageAsync(string language)
        {
            try
            {
                var professionalDataReport = new ProfessionalDataReport();

                professionalDataReport.AboutMe = await _aboutMeRepository.GetByLanguageAsync(language);
                professionalDataReport.WorkList = await _workRepository.GetByLanguageAsync(language);
                professionalDataReport.ProjectList = await _projectRepository.GetByLanguageAsync(language);

                return professionalDataReport;
            }
            catch (Exception e)
            {
                // TODO Log
                throw;
            }
        }
    }
}
