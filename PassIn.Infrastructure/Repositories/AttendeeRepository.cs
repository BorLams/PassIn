using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Infrastructure.Contexts;
using PassIn.Infrastructure.Repositories.Interfaces;

namespace PassIn.Infrastructure.Repositories;
public class AttendeeRepository : IAttendeeRepository
{
    #region Dependency Injections
    private readonly PassInDbContext _context;

    public AttendeeRepository(PassInDbContext context)
    {
        _context = context;
    }
    #endregion

    protected DbSet<Attendee> DbSet => _context.Attendees;

    public async Task<Attendee> RegisterAttendeeAsync(Attendee attendee)
    {
        var registeredAttendee = new Attendee()
        {
            Id = Guid.NewGuid(),
            Name = attendee.Name,
            Email = attendee.Email,
            CreatedAt = DateTime.UtcNow,
            EventId = attendee.EventId
        };

        _ = await DbSet.AddAsync(registeredAttendee);
        _ = await _context.SaveChangesAsync();

        return registeredAttendee;
    }

    public async Task<bool> IsEmailInUseAsync(string email)
        => await DbSet.AnyAsync(a => string.Equals(a.Email, email));

    public async Task<int> CountAttendeesByEventIdAsync(Guid eventId)
        => await DbSet.CountAsync(a => a.EventId == eventId);

    public async Task<IEnumerable<Attendee>> GetAllAsync()
        => await DbSet.Include(a => a.Event)
                      .Include(a => a.CheckIn)
                      .AsNoTracking()
                      .ToListAsync();

    public async Task<IEnumerable<Attendee>> GetAllByEventIdAsync(Guid eventId)
        => await DbSet.Include(a => a.Event)
                      .Include(a => a.CheckIn)
                      .Where(a => a.EventId == eventId)
                      .AsNoTracking()
                      .ToListAsync();

    public async Task<bool> DoesAttendeeExistAsync(Guid id)
        => await DbSet.AnyAsync(a => a.Id == id);
}
