using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Security.Interfaces;
using Support.Models;

namespace Domain.Services;

public class UserService(IUnitOfWork unitOfWork, AppDbContext context, IHashingService hashingService, IBusinessRulesValidator businessRulesValidator) : BaseRepository<User>(context), IUserService
{
    private readonly AppDbContext _context = context;

    public async Task UpdatePassword(int id, string password)
    {
        var userToUpdate = _context.Set<User>().FirstOrDefault(user => user.Id == id);
        if (userToUpdate == null)
        {
            throw new UserNotFoundException();
        }
        businessRulesValidator.Validate(password);
        var newPassword = hashingService.HashPassword(password);
        userToUpdate.UpdatePassword(newPassword);
        await unitOfWork.CompleteAsync();
    }

    public async Task<int> GetUserIdByUsername(string username)
    {
        var user = await _context.Set<User>().FirstOrDefaultAsync(user => user.Username == username);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        return user.Id;
    }
}