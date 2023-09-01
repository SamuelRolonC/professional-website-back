using Core.Entities;
using Core.Interfaces.Services;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class BlogService : IBlogService
    {
        private readonly BlogRepository _blogRepository;

        public BlogService(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            try
            {
                return await _blogRepository.GetAllAsync();
            }
            catch(Exception e)
            {
                // TODO Log
                throw;
            }
        }
    }
}
