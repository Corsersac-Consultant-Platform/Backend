using Domain.Interfaces;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Support.Enums;
using Support.Models;

namespace Domain.Services;

public class SeedStatuesService(IUnitOfWork unitOfWork, IStatusService statusService) : ISeedStatuesService
{
    public async Task SeedStatues()
    {
        foreach (var status in Enum.GetValues<Statues>())
        {
            var statusEntity = await statusService.GetStatusByType(status.ToString());
            if (statusEntity == null)
            {
                var statusToCreate = new Status(status.ToString());
                await statusService.AddAsync(statusToCreate);
                await unitOfWork.CompleteAsync();
            }
           
        }
    }
}