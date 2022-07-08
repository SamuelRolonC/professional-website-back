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

        private readonly IWorkService _workService;
        private readonly IProfessionalDataService _professionalDataService;
        private readonly IEmailSender _emailSender;

        public ClientController(ILogger<ClientController> logger
            , IWorkService workService
            , IProfessionalDataService professionalDataService
            , IEmailSender emailSender)
        {
            _logger = logger;

            _workService = workService;
            _professionalDataService = professionalDataService;
            _emailSender = emailSender;
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

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailFormModel emailFormModel)
        {
            var resultMessage = await _emailSender.SendEmailForContactAsync(
                new EmailMessage(emailFormModel.Name, emailFormModel.Address, emailFormModel.Subject
                , emailFormModel.Content));

            return Json(resultMessage);
        }
    }
}
