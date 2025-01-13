using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Support.Models;

namespace Domain.Services;

public class RoleService(AppDbContext context) : BaseRepository<Role>(context), IRoleService
{
    
}