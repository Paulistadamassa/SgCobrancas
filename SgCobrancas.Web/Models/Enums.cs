namespace SgCobrancas.Web.Models;

public enum InvoiceStatus
{
    Pending,
    Paid,
    Overdue,
    Canceled
}

public enum RecurrenceType
{
    Unica,
    Mensal,
    Anual
}