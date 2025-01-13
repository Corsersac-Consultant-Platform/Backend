using Infrastructure.Persistence.EFC.Repositories;
using Support.Models;

namespace Domain.Interfaces;

public interface IRoleService : IBaseRepository<Role>
{
    Task<Role?> GetRoleByType(string type);
}