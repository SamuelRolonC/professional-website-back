using Core.Entities;
using Core.Interfaces.Services;
using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : CustomApiController
    {
        private readonly ILogger<ClientController> _logger;

        private readonly IProfessionalDataService _professionalDataService;
        private readonly IBlogService _blogService;

        public ClientController(ILogger<ClientController> logger
            , IProfessionalDataService professionalDataService
            , IBlogService blogService)
        {
            _logger = logger;

            _professionalDataService = professionalDataService;
            _blogService = blogService;
        }

        [HttpGet("GetProfessionalData")]
        public async Task<IActionResult> GetProfessionalData(string lang)
        {
            var entirePageViewModel = new EntirePageViewModel();

            try
            {
                var professionalData = await _professionalDataService.GetByLanguageAsync(lang);

                entirePageViewModel = MapTo<ProfessionalDataReport, EntirePageViewModel>(professionalData);
            }
            catch(Exception e)
            {
                entirePageViewModel.ErrorMessage = GetDeepException(e, "Error al obtener los datos. ");
            }

            return Json(entirePageViewModel);
        }

        [HttpGet("GetBlogData")]
        public async Task<IActionResult> GetBlogData()
        {
            var blogDataViewModel = new BlogDataViewModel();

            try
            { 
                var listBlog = await _blogService.GetAllAsync();

                blogDataViewModel.ListBlogViewModel = MapListTo<Blog, BlogViewModel>(listBlog);
            }
            catch (Exception e)
            {
                blogDataViewModel.ListBlogViewModel = new List<BlogViewModel>();
                blogDataViewModel.ErrorMessage = GetDeepException(e, "Error al obtener los datos. ");
            }

            return Json(blogDataViewModel);
        }
    }
}
