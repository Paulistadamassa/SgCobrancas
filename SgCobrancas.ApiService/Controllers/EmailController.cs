using Microsoft.AspNetCore.Mvc;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Services;
using System.Reflection.Metadata.Ecma335;

namespace SgCobrancas.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmail(EmailDTO email)
        {
            var returnEmail = await _emailService.CreateEmail(email);
            if (returnEmail == null) { return BadRequest(); }
            return Ok(returnEmail);
        }
    }
}
