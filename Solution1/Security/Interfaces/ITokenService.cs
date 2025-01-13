using Support.Models;

namespace Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, Role role);
    Task<int?> ValidateToken(string token);
}