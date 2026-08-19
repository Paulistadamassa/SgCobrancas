using Microsoft.AspNetCore.Mvc;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Services;

namespace SgCobrancas.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;
    private readonly ICustomerService _customerService;
    private readonly IEmailService _emailService;

    public InvoicesController(IInvoiceService invoiceService, ICustomerService customerService, IEmailService emailService)
    {
        _invoiceService = invoiceService;
        _customerService = customerService;
        _emailService = emailService;
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

    [HttpPost("{id}/enviar-email")]
    public async Task<IActionResult> EnviarEmail(int id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);
        if (invoice == null)
            return NotFound("Fatura não encontrada.");

        var customer = await _customerService.GetByIdAsync(invoice.CustomerId);
        if (customer == null || string.IsNullOrWhiteSpace(customer.Email))
            return BadRequest("Cliente sem e-mail cadastrado.");

        var assunto = $"Fatura - {invoice.Status} - Vencimento {invoice.DataVencimento:dd/MM/yyyy}";
        var corpo = $"Olá {customer.Name}, sua fatura no valor de {invoice.Valor:C} vence em {invoice.DataVencimento:dd/MM/yyyy}.";

        var sucesso = await _emailService.EnviarMensagemAsync(customer.Email, assunto, corpo);
        if (!sucesso)
            return StatusCode(500, "Falha ao enviar o e-mail.");

        return Ok();
    }
}