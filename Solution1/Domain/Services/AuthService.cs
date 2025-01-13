using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Security.Interfaces;
using Support.Models;

namespace Domain.Services;

public class AuthService(IUnitOfWork unitOfWork, AppDbContext context, ITokenService tokenService, IHashingService hashingService ) : BaseRepository<User>(context), IAuthService
{
    public async Task<bool> SignUp(string username, string password)
    {
        var user = new User
        {
            Username = username,
            Password = hashingService.HashPassword(password),
            RoleId = 2
        };

        await AddAsync(user);
        await unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<string> SignIn(string username, string password)
    {
        var user = await context.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
        if (user == null || !hashingService.VerifyPassword(password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var role = await context.Set<Role>().FirstOrDefaultAsync(x => x.Id == user.RoleId);
        return tokenService.GenerateToken(user, role);
    }

    public async Task<string> RefreshToken(string token)
    {
        var userId = await tokenService.ValidateToken(token);
        if (userId == null)
        {
            throw new InvalidCredentialsException();
        }

        var user = await context.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        var role = await context.Set<Role>().FirstOrDefaultAsync(x => x.Id == user.RoleId);
        return tokenService.GenerateToken(user, role);
    }
}