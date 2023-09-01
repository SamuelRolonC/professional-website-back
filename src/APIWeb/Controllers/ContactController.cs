using Core;
using Core.Entities;
using Core.Interfaces.Services;
using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : CustomApiController
    {
        private readonly ILogger<ContactController> _logger;

        private readonly IContactService _contactService;

        public ContactController(ILogger<ContactController> logger
            , IContactService contactService)
        {
            _logger = logger;

            _contactService = contactService;
        }

        [HttpPost("ProcessForm")]
        public async Task<IActionResult> ProcessForm([FromBody] EmailFormModel emailFormModel)
        {
            var resultData = new ResultData();
            try
            {
                var contact = MapTo<EmailFormModel, Contact>(emailFormModel);
                resultData = await _contactService.ProcessFormAsync(contact);
                return Json(resultData);
            }
            catch(Exception e)
            {
                resultData.AddError("Fatal", GetDeepException(e, ""));
                return Json(resultData);
            }

        }
    }
}
