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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmailById(int id)
        {
            var email = await _emailService.GetEmailById(id);
            if (email == null) { return NotFound(); }
            return Ok(email);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditEmail(int id, EmailDTO email)
        {
            var returnEmail = await _emailService.EditEmail(id, email);
            if (returnEmail == null) { return NotFound(); }
            return Ok(returnEmail);
        }
    }
}
