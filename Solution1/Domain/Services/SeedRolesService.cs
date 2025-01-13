using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Support.Enums;
using Support.Models;

namespace Domain.Services;

public class SeedRolesService(AppDbContext context, IUnitOfWork unitOfWork) : ISeedRolesService
{
    public async Task SeedRoles()
    {
        foreach (var role in Enum.GetValues<Roles>())
        {
            var roleEntity = new Role
            {
                Type = role.ToString()
            };

            if (context.Set<Role>().Any(x => x.Type == roleEntity.Type)) continue;
            await context.Set<Role>().AddAsync(roleEntity);
            await unitOfWork.CompleteAsync();
        }
    }
}