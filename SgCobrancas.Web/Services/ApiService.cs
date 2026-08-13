using System.Net.Http.Json;
using SgCobrancas.Web.DTOs;

namespace SgCobrancas.Web.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _http;

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CustomerDTO>> GetCustomersAsync()
    {
        var response = await _http.GetFromJsonAsync<List<CustomerDTO>>("api/Customers");
        return response ?? new List<CustomerDTO>();
    }

    public async Task<CustomerDTO?> GetCustomerByIdAsync(int? id)
    {
        if (id is null) return null;
        return await _http.GetFromJsonAsync<CustomerDTO>($"api/Customers/{id}");
    }

    public async Task<CustomerDTO?> CreateCustomerAsync(CustomerDTO customer)
    {
        var response = await _http.PostAsJsonAsync("api/Customers", customer);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<CustomerDTO>();
    }

    public async Task<CustomerDTO?> UpdateCustomerAsync(int? id, CustomerDTO customer)
    {
        if (id is null) return null;
        var response = await _http.PutAsJsonAsync($"api/Customers/{id}", customer);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<CustomerDTO>();
    }

    public async Task<bool?> DeleteCustomerAsync(int? id)
    {
        if (id is null) return false;
        var response = await _http.DeleteAsync($"api/Customers/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<InvoiceDTO>> GetInvoicesAsync()
    {
        var response = await _http.GetFromJsonAsync<List<InvoiceDTO>>("api/invoice");
        return response ?? new List<InvoiceDTO>();
    }

    public async Task<InvoiceDTO?> GetInvoiceByIdAsync(int? id)
    {
        if (id is null) return null;
        return await _http.GetFromJsonAsync<InvoiceDTO>($"api/invoice/{id}");
    }

    public async Task<InvoiceDTO?> CreateInvoiceAsync(InvoiceDTO invoice)
    {
        var response = await _http.PostAsJsonAsync("api/invoice", invoice);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<InvoiceDTO>();
    }

    public async Task<InvoiceDTO?> UpdateInvoiceAsync(int? id, InvoiceDTO invoice)
    {
        if (id is null) return null;
        var response = await _http.PutAsJsonAsync($"api/invoice/{id}", invoice);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<InvoiceDTO>();
    }

    public async Task<bool?> DeleteInvoiceAsync(int? id)
    {
        if (id is null) return false;
        var response = await _http.DeleteAsync($"api/invoice/{id}");
        return response.IsSuccessStatusCode;
    }
}