using Infrastructure.Persistence.EFC.Repositories;
using Support.Models;

namespace Domain.Interfaces;

public interface IStatusService : IBaseRepository<Status>
{
    Task<Status?> GetStatusByType(string type);
}