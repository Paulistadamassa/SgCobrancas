using SgCobrancas.Web.DTOs;

namespace SgCobrancas.Web.Services;

public interface IApiService
{
    // Clientes
    Task<List<CustomerDTO>> GetCustomersAsync();
    Task<CustomerDTO?> GetCustomerByIdAsync(int? id);
    Task<CustomerDTO?> CreateCustomerAsync(CustomerDTO customer);
    Task<CustomerDTO?> UpdateCustomerAsync(int? id, CustomerDTO customer);
    Task<bool?> DeleteCustomerAsync(int? id);

    // Faturas
    Task<List<InvoiceDTO>> GetInvoicesAsync();
    Task<InvoiceDTO?> GetInvoiceByIdAsync(int? id);
    Task<InvoiceDTO?> CreateInvoiceAsync(InvoiceDTO invoice);
    Task<InvoiceDTO?> UpdateInvoiceAsync(int? id, InvoiceDTO invoice);
    Task<bool?> DeleteInvoiceAsync(int? id);
    Task<bool> EnviarEmailFaturaAsync(int id);

    Task<EmailDTO?> EditEmailAsync(int? id, EmailDTO email);
    Task<EmailDTO>? GetEmailByIdAsync(int? id);
}