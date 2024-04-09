using PassIn.Application.Data.Dtos.Attendee.Request;
using PassIn.Application.Data.Dtos.Attendee.Response;
using PassIn.Application.Validations;

namespace PassIn.Application.Services.Interfaces;

public interface IAttendeesService
{
    Task<IEnumerable<ResponseAttendeeDto>> GetAllAsync();
    Task<IEnumerable<ResponseAttendeeDto>> GetAllByEventIdAsync(Guid eventId);
    Task<(ValidationResult, ResponseAttendeeDto)> RegisterAttendeeAsync(RequestRegisterAttendeeDto request);
}
