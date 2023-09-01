using Core.Entities;
using Core.Interfaces.Services;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class WorkService : IWorkService
    {
        private readonly WorkRepository _workRepository;

        public WorkService(WorkRepository workRepository)
        {
            _workRepository = workRepository;
        }
        public async Task<List<Work>> GetByLanguageAsync(string language)
        {
            try
            {
                return await _workRepository.GetByLanguageAsync(language);
            }
            catch(Exception e)
            {
                // TODO Log
                throw;
            }
        }
    }
}
