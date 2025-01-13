using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Support.Models;

namespace Domain.Services;

public class RoleService(AppDbContext context) : BaseRepository<Role>(context), IRoleService
{

    public async Task<Role?> GetRoleByType(string type)
    {
        return await context.Set<Role>().FirstOrDefaultAsync(role => role.Type == type);
    }
}