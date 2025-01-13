using Domain.Interfaces;
using Infrastructure.Persistence.EFC;
using Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Support.Models;

namespace Domain.Services;

public class StatusService(AppDbContext context) : BaseRepository<Status>(context), IStatusService
{
    private readonly AppDbContext _context = context;

    public async Task<Status?> GetStatusByType(string type)
    {
        return await _context.Set<Status>().FirstOrDefaultAsync(status => status.Type == type);
    }
}