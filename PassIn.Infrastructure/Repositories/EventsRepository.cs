using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Infrastructure.Contexts;
using PassIn.Infrastructure.Repositories.Interfaces;

namespace PassIn.Infrastructure.Repositories;
public class EventsRepository : IEventsRepository
{
    #region Dependency Injections
    private readonly PassInDbContext _context;

    public EventsRepository(PassInDbContext context)
    {
        _context = context;
    }
    #endregion

    protected DbSet<Event> DbSet => _context.Events;

    public async Task<Event> CreateEventAsync(Event eventToRegister)
    {
        var eventRegistered = new Event()
        {
            Id = Guid.NewGuid(),
            Title = eventToRegister.Title,
            Details = eventToRegister.Details,
            MaximumAttendees = eventToRegister.MaximumAttendees,
            Slug = eventToRegister.Title.ToLower().Replace(" ", "-")
        };

        _ = await DbSet.AddAsync(eventRegistered);
        _ = await _context.SaveChangesAsync();

        return eventRegistered;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
        => await DbSet.Include(e => e.Attendees)
                      .AsNoTracking()
                      .ToListAsync();

    public async Task<Event> GetEventByIdAsync(Guid id)
        => await DbSet.Include(e => e.Attendees)
                      .AsNoTracking()
                      .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<bool> DoesEventExistAsync(Guid id)
        => await DbSet.AnyAsync(e => e.Id == id);
}
