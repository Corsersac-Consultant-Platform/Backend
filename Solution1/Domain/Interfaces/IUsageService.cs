using System.Collections;
using Infrastructure.Persistence.EFC.Repositories;
using Support.Models;

namespace Domain.Interfaces;

public interface IUsageService : IBaseRepository<Usage>
{
    Task<IEnumerable<Usage>> GetUsagesByRange(DateTime start, DateTime end);
    Task<IEnumerable<Usage>> GetUsagesByDate(DateTime date);
    Task<Hashtable> CountTotalUsagesByYear(int year);
    Task<IEnumerable<Usage>> GetUsagesByVehicleIdentifier(string vehicleIdentifier);
    Task<bool> CreateUsage(Usage usage);
}