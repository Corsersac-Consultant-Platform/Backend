using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Support.Models;

namespace Domain.Services;

public class InvoiceService(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<Invoice>(context), IInvoiceService
{
    public async Task<bool> CreateInvoice(Invoice invoice)
    {
        businessRulesValidator.Validate(invoice);
        await AddAsync(invoice);
        await unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateInvoiceStatus(int statusId, int id)
    {
        var invoice = await GetByIdAsync(id);
        if (invoice == null)
        {
            throw new Exception("Invoice not found");
        }
        invoice.UpdateStatus(statusId);
        await unitOfWork.CompleteAsync();
        return true;
        
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByRange(DateTime start, DateTime end)
    {
        var invoices = await context.Set<Invoice>()
            .Where(invoice => 
                (invoice.EmitDate.Year > start.Year || 
                 (invoice.EmitDate.Year == start.Year && invoice.EmitDate.Month > start.Month) || 
                 (invoice.EmitDate.Year == start.Year && invoice.EmitDate.Month == start.Month && invoice.EmitDate.Day >= start.Day))
                &&
                (invoice.EmitDate.Year < end.Year || 
                 (invoice.EmitDate.Year == end.Year && invoice.EmitDate.Month < end.Month) || 
                 (invoice.EmitDate.Year == end.Year && invoice.EmitDate.Month == end.Month && invoice.EmitDate.Day <= end.Day))
            )
            .ToListAsync();

        return invoices;
    }


    public async Task<IEnumerable<Invoice>> GetInvoicesByDate(DateTime date)
    {
        var invoices = await context.Set<Invoice>()
            .Where(invoice => 
                invoice.EmitDate.Year == date.Year && 
                invoice.EmitDate.Month == date.Month && 
                invoice.EmitDate.Day == date.Day
            )
            .ToListAsync();

        return invoices;
    }


    public async Task<IEnumerable<Invoice>> GetInvoicesBySerie(string serie)
    {
        var invoices = await context.Set<Invoice>().Where(invoice => invoice.Serie == serie).ToListAsync();
        return invoices;
    }
}