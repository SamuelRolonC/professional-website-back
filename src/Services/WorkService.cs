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

        public Work Create(Work work)
        {
            throw new NotImplementedException();
        }

        public List<Work> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Work>> GetAsync()
        {
            try
            {
                return await _workRepository.GetAsync();
            }
            catch(Exception e)
            {
                // TODO Log
                throw;
            }
        }

        public Work Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Work workIn)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, Work workIn)
        {
            throw new NotImplementedException();
        }

        public Task<Work> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Work> CreateAsync(Work work)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(string id, Work workIn)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(Work workIn)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
