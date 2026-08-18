namespace SgCobrancas.ApiService.Models;

public enum InvoiceStatus
{
    Pendente = 0,
    Pago = 1,
    Vencido = 2,
    Cancelado = 3
}

public enum RecurrenceType
{
    Unica = 0,
    Semanal = 1,
    Mensal = 2,
    Bimestral = 3,
    Trimestral = 4,
    Semestral = 5,
    Anual = 6
}

public class Invoice
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pendente;
    public RecurrenceType Recorrencia { get; set; } = RecurrenceType.Unica;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataPagamento { get; set; }
    public bool LembretePrevioEnviado { get; set; } = false;
    public bool EmailAtrasoEnviado { get; set; } = false;
}