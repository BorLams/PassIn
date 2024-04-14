using PassIn.Domain.Entities;

namespace PassIn.Infrastructure.Repositories.Interfaces;
public interface IAttendeeRepository
{
    Task<IEnumerable<Attendee>> GetAllAsync();
    Task<IEnumerable<Attendee>> GetAllByEventIdAsync(Guid eventId);
    Task<Attendee> RegisterAttendeeAsync(Attendee attendee);
    Task<int> CountAttendeesByEventIdAsync(Guid eventId);
    Task<bool> IsEmailInUseAsync(string email);
    Task<bool> DoesAttendeeExistAsync(Guid id);
}
