using SgCobrancas.ApiService.DTOs;

namespace SgCobrancas.ApiService.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDTO>> GetAllAsync();
    Task<CustomerDTO?> GetByIdAsync(int id);
    Task<CustomerDTO> CreateAsync(CustomerDTO request);
    Task<CustomerDTO?> UpdateAsync(int id, CustomerDTO request);
    Task<bool> DeleteAsync(int id);
}