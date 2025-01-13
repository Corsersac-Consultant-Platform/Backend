using Infrastructure.Persistence.EFC.Repositories;
using Support.Models;

namespace Domain.Interfaces;

public interface IInvoiceService : IBaseRepository<Invoice>
{
    Task<bool> CreateInvoice(Invoice invoice);
    Task<bool> UpdateInvoiceStatus(int statusId, int id);
    Task<IEnumerable<Invoice>> GetInvoicesByRange(DateTime start, DateTime end);
    Task<IEnumerable<Invoice>> GetInvoicesByDate(DateTime date);
    Task<IEnumerable<Invoice>> GetInvoicesBySerie(string serie);
}