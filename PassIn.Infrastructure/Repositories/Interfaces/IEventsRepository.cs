using PassIn.Domain.Entities;

namespace PassIn.Infrastructure.Repositories.Interfaces;
public interface IEventsRepository
{
    Task<Event> GetEventByIdAsync(Guid id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event> CreateEventAsync(Event eventRegistered);
    Task<bool> DoesEventExistAsync(Guid id);
}
