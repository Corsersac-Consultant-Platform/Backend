using Infrastructure.Persistence.EFC.Repositories;
using Support.Models;

namespace Domain.Interfaces;

public interface IAuthService : IBaseRepository<User>
{
    Task<bool> SignUp(string username, string password);
    Task<string> SignIn(string username, string password);
    
    Task<string> RefreshToken(string token);
}