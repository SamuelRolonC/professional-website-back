using Core.Entities;
using Core.Interfaces.Services;
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

        private readonly IWorkService _workService;

        public ClientController(ILogger<ClientController> logger
            , IWorkService workService)
        {
            _logger = logger;

            _workService = workService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entirePageViewModel = new EntirePageViewModel();

            entirePageViewModel.AboutMeViewModel = new AboutMeViewModel()
            {
                Title = "I'm Samuel",
                Text = "Lorem ipsum. Lorem ipsum. Salum."
            };

            return Json(entirePageViewModel);
        }

        [HttpGet("GetProfessionalData")]
        public async Task<IActionResult> GetProfessionalData()
        {
            var entirePageViewModel = new EntirePageViewModel();

            try
            {
                entirePageViewModel.AboutMeViewModel = new AboutMeViewModel()
                {
                    Title = "I'm Samuel",
                    Text = "Lorem ipsum. Lorem ipsum. Salum."
                };

                var workList = await _workService.GetAsync();
                entirePageViewModel.ListWorkViewModel = MapListTo<Work, WorkViewModel>(workList);
            }
            catch(Exception e)
            {
                entirePageViewModel.ErrorMessage = GetDeepException(e, "Error al obtener los datos. ");
            }

            return Json(entirePageViewModel);
        }
    }
}
