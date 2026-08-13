using Microsoft.EntityFrameworkCore;
using SgCobrancas.ApiService.Data;
using SgCobrancas.ApiService.DTOs;
using SgCobrancas.ApiService.Models;

namespace SgCobrancas.ApiService.Services;

public class InvoiceService : IInvoiceService
{
    private readonly AppDbContext _context;

    public InvoiceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvoiceDTO>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(i => i.Customer)
            .Select(i => new InvoiceDTO
            {
                Id = i.Id,
                Valor = i.Valor,
                DataVencimento = i.DataVencimento,
                Status = i.Status,
                Recorrencia = i.Recorrencia,
                DataCriacao = i.DataCriacao,
                DataPagamento = i.DataPagamento,
                CustomerId = i.CustomerId,
                CustomerName = i.Customer != null ? i.Customer.Name : string.Empty
            })
            .ToListAsync();
    }

    public async Task<InvoiceDTO?> GetByIdAsync(int id)
    {
        var i = await _context.Invoices
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (i == null) return null;

        return new InvoiceDTO
        {
            Id = i.Id,
            Valor = i.Valor,
            DataVencimento = i.DataVencimento,
            Status = i.Status,
            Recorrencia = i.Recorrencia,
            DataCriacao = i.DataCriacao,
            DataPagamento = i.DataPagamento,
            CustomerId = i.CustomerId,
            CustomerName = i.Customer != null ? i.Customer.Name : string.Empty
        };
    }

    public async Task<InvoiceDTO?> CreateAsync(InvoiceDTO request)
    {
        var customer = await _context.Customers.FindAsync(request.CustomerId);
        if (customer == null) return null;

        var invoice = new Invoice
        {
            Valor = request.Valor,
            DataVencimento = request.DataVencimento,
            CustomerId = request.CustomerId,
            Recorrencia = request.Recorrencia,
            Status = InvoiceStatus.Pending
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return new InvoiceDTO
        {
            Id = invoice.Id,
            Valor = invoice.Valor,
            DataVencimento = invoice.DataVencimento,
            Status = invoice.Status,
            Recorrencia = invoice.Recorrencia,
            DataCriacao = invoice.DataCriacao,
            DataPagamento = invoice.DataPagamento,
            CustomerId = customer.Id,
            CustomerName = customer.Name
        };
    }

    public async Task<InvoiceDTO?> UpdateAsync(int id, InvoiceDTO request)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (invoice == null) return null;

        // Se o cliente mudou, valida a existência do novo cliente
        if (invoice.CustomerId != request.CustomerId)
        {
            var newCustomer = await _context.Customers.FindAsync(request.CustomerId);
            if (newCustomer == null) return null;
            invoice.CustomerId = request.CustomerId;
            invoice.Customer = newCustomer;
        }

        invoice.Valor = request.Valor;
        invoice.DataVencimento = request.DataVencimento;
        invoice.Status = request.Status;
        invoice.Recorrencia = request.Recorrencia;
        invoice.DataPagamento = request.DataPagamento;

        await _context.SaveChangesAsync();

        return new InvoiceDTO
        {
            Id = invoice.Id,
            Valor = invoice.Valor,
            DataVencimento = invoice.DataVencimento,
            Status = invoice.Status,
            Recorrencia = invoice.Recorrencia,
            DataCriacao = invoice.DataCriacao,
            DataPagamento = invoice.DataPagamento,
            CustomerId = invoice.CustomerId,
            CustomerName = invoice.Customer != null ? invoice.Customer.Name : string.Empty
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null) return false;

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        return true;
    }
}