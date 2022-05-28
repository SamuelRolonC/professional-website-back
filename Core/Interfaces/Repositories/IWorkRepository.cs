using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IWorkRepository
    {
        List<Work> Get();
        Task<List<Work>> GetAsync();
        Work Get(string id);
        Task<Work> GetAsync(string id);
        Work Create(Work work);
        Task<Work> CreateAsync(Work work);
        void Update(string id, Work workIn);
        void UpdateAsync(string id, Work workIn);
        void Remove(Work workIn);
        void RemoveAsync(Work workIn);
        void Remove(string id);
        void RemoveAsync(string id);
    }
}
