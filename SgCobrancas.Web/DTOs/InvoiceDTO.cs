using SgCobrancas.Web.Models;

namespace SgCobrancas.Web.DTOs;

public class InvoiceDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
    public RecurrenceType Recorrencia { get; set; } = RecurrenceType.Unica;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataPagamento { get; set; }
    public string? CustomerName { get; set; }
}