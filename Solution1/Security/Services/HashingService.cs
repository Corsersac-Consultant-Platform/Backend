
using Security.Interfaces;
namespace Security.Services;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
       return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}