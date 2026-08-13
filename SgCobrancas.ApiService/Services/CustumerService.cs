using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SgCobrancas.ApiService.Data;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Models;

namespace SgCobrancas.ApiService.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
    }

    public async Task<CustomerDTO?> GetByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;
        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task<CustomerDTO> CreateAsync(CustomerDTO request)
    {
        var entity = _mapper.Map<Customer>(request);

        _context.Customers.Add(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerDTO>(entity);
    }

    public async Task<CustomerDTO?> UpdateAsync(int id, CustomerDTO request)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;

        // Mapeia os dados do DTO diretamente para a entidade rastreada pelo EF
        _mapper.Map(request, customer);
        customer.Id = id; // Preserva o ID original da rota

        await _context.SaveChangesAsync();
        return _mapper.Map<CustomerDTO>(customer);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}