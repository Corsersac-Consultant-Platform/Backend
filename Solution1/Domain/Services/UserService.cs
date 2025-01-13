using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Infrastructure.Persistence.EFC.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Security.Interfaces;
using Support.Models;

namespace Domain.Services;

public class UserService(IUnitOfWork unitOfWork, AppDbContext context, IHashingService hashingService) : BaseRepository<User>(context), IUserService
{
    private readonly AppDbContext _context = context;

    public async Task UpdatePassword(int id, string password)
    {
        var userToUpdate = _context.Set<User>().FirstOrDefault(user => user.Id == id);
        if (userToUpdate == null)
        {
            throw new UserNotFoundException();
        }
        var newPassword = hashingService.HashPassword(password);
        userToUpdate.UpdatePassword(newPassword);
        await unitOfWork.CompleteAsync();
    }
}