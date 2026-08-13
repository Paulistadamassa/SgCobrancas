using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Models;

namespace SgCobrancas.ApiService.Services;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceDTO>> GetAllAsync();
    Task<InvoiceDTO?> GetByIdAsync(int id);
    Task<InvoiceDTO?> CreateAsync(InvoiceDTO request);
    Task<InvoiceDTO?> UpdateAsync(int id, InvoiceDTO request);
    Task<bool> DeleteAsync(int id);
}