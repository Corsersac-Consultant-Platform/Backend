using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Support.Enums;
using Support.Models;

namespace Domain.Services;

public class SeedRolesService(IRoleService roleService, IUnitOfWork unitOfWork) : ISeedRolesService
{
    public async Task SeedRoles()
    {
        foreach (var role in Enum.GetValues<Roles>())
        {
            var roleEntity = await roleService.GetRoleByType(role.ToString());
            if (roleEntity == null)
            {
                var roleToCreate = new Role(role.ToString());
                await roleService.AddAsync(roleToCreate);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}