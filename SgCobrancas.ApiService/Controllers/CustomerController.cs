using Microsoft.AspNetCore.Mvc;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Services;

namespace SgCobrancas.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
    {
        return Ok(await _customerService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDTO>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null) return NotFound("Cliente não encontrado.");
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> Create([FromBody] CustomerDTO request)
    {
        var created = await _customerService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDTO>> Update(int id, [FromBody] CustomerDTO request)
    {
        if (id != request.Id && request.Id != 0)
        {
            return BadRequest("O ID da URL não coincide com o ID do corpo da requisição.");
        }

        var updated = await _customerService.UpdateAsync(id, request);
        if (updated == null)
        {
            return NotFound("Cliente não encontrado para atualização.");
        }

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _customerService.DeleteAsync(id);
        if (!success)
        {
            return NotFound("Cliente não encontrado para exclusão.");
        }

        return NoContent();
    }

    [HttpPost("/enviar-email")]
    public async Task<ActionResult<bool>> EnviarEmail(int id, string assunto, string corpo)
    {
        var success = await _customerService.EnviarEmailCobrando(id, assunto, corpo);
        if (!success)
        {
            return NotFound("Cliente não encontrado ou erro ao enviar email.");
        }

        return Ok(true);
    }
}