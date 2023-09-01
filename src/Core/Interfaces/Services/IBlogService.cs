using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
    }
}
