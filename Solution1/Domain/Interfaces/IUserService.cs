using Infrastructure.Persistence.EFC.Repositories;
using Support.Models;

namespace Domain.Interfaces;

public interface IUserService : IBaseRepository<User>
{
    Task UpdatePassword(int id, string password);
}