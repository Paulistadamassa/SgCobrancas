using Microsoft.AspNetCore.Mvc;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Services;

namespace SgCobrancas.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetAll()
    {
        return Ok(await _invoiceService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDTO>> GetById(int id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);
        if (invoice == null) return NotFound("Fatura não encontrada.");
        return Ok(invoice);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDTO>> Create([FromBody] InvoiceDTO request)
    {
        var created = await _invoiceService.CreateAsync(request);
        if (created == null) return BadRequest("O cliente informado não existe.");

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InvoiceDTO>> Update(int id, [FromBody] InvoiceDTO request)
    {
        if (id != request.Id && request.Id != 0)
        {
            return BadRequest("O ID da URL não coincide com o ID do corpo da requisição.");
        }

        var updated = await _invoiceService.UpdateAsync(id, request);
        if (updated == null)
        {
            return NotFound("Fatura não encontrada ou cliente informado é inválido.");
        }

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _invoiceService.DeleteAsync(id);
        if (!success)
        {
            return NotFound("Fatura não encontrada para exclusão.");
        }

        return NoContent();
    }
}