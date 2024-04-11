using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Infrastructure.Contexts;
using PassIn.Infrastructure.Repositories.Interfaces;

namespace PassIn.Infrastructure.Repositories;
public class CheckInsRepository : ICheckInsRepository
{
    #region Repository Initialization
    private readonly PassInDbContext _context;

    public CheckInsRepository(PassInDbContext context)
    {
        _context = context;
    }
    protected DbSet<CheckIn> DbSet => _context.CheckIns;
    #endregion

    public async Task<IEnumerable<CheckIn>> GetAllAsync()
        => await DbSet.Include(c => c.Attendee)
                        .ThenInclude(a => a.Event)
                      .AsNoTracking()
                      .ToListAsync();

    public async Task<CheckIn> GetCheckInByIdAsync(Guid id)
    => await DbSet.Include(c => c.Attendee)
                    .ThenInclude(a => a.Event)
                  .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<CheckIn> CheckInAsync(CheckIn checkIn)
    {
        var checkInToRegister = new CheckIn()
        {
            Id = Guid.NewGuid(),
            AttendeeId = checkIn.AttendeeId,
            CheckedInAt = DateTime.UtcNow
        };

        _ = await DbSet.AddAsync(checkInToRegister);
        _ = await _context.SaveChangesAsync();

        var registeredCheckIn = await GetCheckInByIdAsync(checkInToRegister.Id);

        return registeredCheckIn;
    }
}
