namespace SgCobrancas.ApiService.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty; // CPF ou CNPJ
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }

    // Relacionamento com Faturas
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}