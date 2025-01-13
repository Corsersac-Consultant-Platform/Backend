using System.Collections;
using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Support.Models;

namespace Domain.Services;

public class UsageService(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<Usage>(context), IUsageService
{
    public async Task<IEnumerable<Usage>> GetUsagesByRange(DateTime start, DateTime end)
    {
        return await context.Set<Usage>().Where(usage =>
            (usage.Date.Year > start.Year || (usage.Date.Year == start.Year && usage.Date.Month > start.Month) || 
             (usage.Date.Year == start.Year && usage.Date.Month == start.Month && usage.Date.Day >= start.Day)) &&
            (usage.Date.Year < end.Year || (usage.Date.Year == end.Year && usage.Date.Month < end.Month) || 
             (usage.Date.Year == end.Year && usage.Date.Month == end.Month && usage.Date.Day <= end.Day))
        ).ToListAsync();
    }


    public async Task<IEnumerable<Usage>> GetUsagesByDate(DateTime date)
    {
        return await context.Set<Usage>().Where(usage =>
            usage.Date.Year == date.Year && usage.Date.Month == date.Month && usage.Date.Day == date.Day
        ).ToListAsync();
    }

    public async Task<Hashtable> CountTotalUsagesByYear(int year)
    {
        var relations = new Hashtable();
        
        var usages = await context.Set<Usage>()
            .Where(usage => usage.Date.Year == year)
            .ToListAsync();

        foreach (var usage in usages)
        {
          
            if (!relations.ContainsKey(usage.UsageCenter))
            {
                relations[usage.UsageCenter] = new Hashtable();
                relations[usage.UsageCenter] = 0;
            }
            
            relations[usage.UsageCenter] = (int)relations[usage.UsageCenter] + 1;
        }

        return relations;
    }



    public async Task<IEnumerable<Usage>> GetUsagesByVehicleIdentifier(string vehicleIdentifier)
    {
        return await context.Set<Usage>().Where(usage => usage.VehicleIdentifier == vehicleIdentifier).ToListAsync();
    }

    public async Task<bool> CreateUsage(Usage usage)
    {
        businessRulesValidator.Validate(usage);
        await context.Set<Usage>().AddAsync(usage);
        await unitOfWork.CompleteAsync();
        return true;
    }
}