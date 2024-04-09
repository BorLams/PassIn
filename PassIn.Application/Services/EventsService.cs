using AutoMapper;
using PassIn.Application.Data.Dtos.Event.Request;
using PassIn.Application.Data.Dtos.Event.Response;
using PassIn.Application.Services.Interfaces;
using PassIn.Application.Validations;
using PassIn.Domain.Entities;
using PassIn.Infrastructure.Repositories.Interfaces;

namespace PassIn.Application.Services;
public class EventsService : IEventsService
{
    #region DependencyInjections
    private readonly IEventsRepository _repository;
    private readonly IMapper _mapper;

    public EventsService(IEventsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    #endregion

    public async Task<ResponseEventDto> GetByIdAsync(Guid id)
        => _mapper.Map<ResponseEventDto>(await _repository.GetEventByIdAsync(id));

    public async Task<IEnumerable<ResponseEventDto>> GetAllAsync()
        => _mapper.Map<IEnumerable<ResponseEventDto>>(await _repository.GetAllAsync());

    public async Task<(ValidationResult, ResponseRegisteredEventDto)> RegisterEventAsync(RequestEventDto request)
    {
        ResponseRegisteredEventDto response = null;

        var validation = Validate(request);
        if (validation.IsValid)
        {
            var eventModel = _mapper.Map<Event>(request);

            response = _mapper.Map<ResponseRegisteredEventDto>(await _repository.CreateEventAsync(eventModel));
        }

        return (validation, response);
    }

    public async Task<bool> DoesEventExistAsync(Guid id)
        => await _repository.DoesEventExistAsync(id);

    #region Private Methods
    private static ValidationResult Validate(RequestEventDto request)
    {
        ValidationResult validation = new();

        if (request.MaximumAttendees <= 0)
        {
            validation.AddValidation($"{nameof(request.MaximumAttendees)} cannot be less than or equal to zero.");
        }
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            validation.AddValidation($"{nameof(request.Title)} cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(request.Details))
        {
            validation.AddValidation($"{nameof(request.Details)} cannot be empty.");
        }

        return validation;
    }
    #endregion
}
