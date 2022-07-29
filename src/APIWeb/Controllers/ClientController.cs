using Core.Entities;
using Core.Interfaces.Services;
using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : CustomApiController
    {
        private readonly ILogger<ClientController> _logger;

        private readonly IProfessionalDataService _professionalDataService;

        public ClientController(ILogger<ClientController> logger
            , IProfessionalDataService professionalDataService)
        {
            _logger = logger;

            _professionalDataService = professionalDataService;
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
    }
}
