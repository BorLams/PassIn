using PassIn.Application.Data.Dtos.Event.Request;
using PassIn.Application.Data.Dtos.Event.Response;
using PassIn.Application.Validations;

namespace PassIn.Application.Services.Interfaces;

public interface IEventsService
{
    Task<bool> DoesEventExistAsync(Guid id);
    Task<IEnumerable<ResponseEventDto>> GetAllAsync();
    Task<ResponseEventDto> GetByIdAsync(Guid id);
    Task<(ValidationResult, ResponseRegisteredEventDto)> RegisterEventAsync(RequestEventDto request);
}
