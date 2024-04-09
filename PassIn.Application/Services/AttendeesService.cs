using AutoMapper;
using PassIn.Application.Data.Dtos.Attendee.Request;
using PassIn.Application.Data.Dtos.Attendee.Response;
using PassIn.Application.Services.Interfaces;
using PassIn.Application.Validations;
using PassIn.Domain.Entities;
using PassIn.Infrastructure.Repositories.Interfaces;
using System.Net.Mail;

namespace PassIn.Application.Services;

public class AttendeesService : IAttendeesService
{
    #region Dependency Injections
    private readonly IEventsService _eventsService;
    private readonly IAttendeeRepository _repository;
    private readonly IMapper _mapper;

    public AttendeesService(IAttendeeRepository repository, IMapper mapper, IEventsService eventsService)
    {
        _repository = repository;
        _mapper = mapper;
        _eventsService = eventsService;
    }
    #endregion

    public async Task<(ValidationResult, ResponseAttendeeDto)> RegisterAttendeeAsync(RequestRegisterAttendeeDto request)
    {
        ResponseAttendeeDto response = null;
        var validation = await Validate(request);

        if (validation.IsValid)
        {
            var registeredAttendee = await _repository.RegisterAttendeeAsync(_mapper.Map<Attendee>(request));
            response = _mapper.Map<ResponseAttendeeDto>(registeredAttendee);
        }

        return (validation, response);
    }

    public async Task<IEnumerable<ResponseAttendeeDto>> GetAllAsync()
        => _mapper.Map<IEnumerable<ResponseAttendeeDto>>(await _repository.GetAllAsync());

    public async Task<IEnumerable<ResponseAttendeeDto>> GetAllByEventIdAsync(Guid eventId)
        => _mapper.Map<IEnumerable<ResponseAttendeeDto>>(await _repository.GetAllByEventIdAsync(eventId));
    #region Private Methods
    private async Task<ValidationResult> Validate(RequestRegisterAttendeeDto request)
    {
        var validation = new ValidationResult();

        if (string.IsNullOrWhiteSpace(request.Name))
            validation.AddValidation("Attendee's name cannot be empty.");

        #region Event
        if (Equals(request.EventId, Guid.Empty))
            validation.AddValidation("Attendee's event id cannot be empty.");

        else if (!await _eventsService.DoesEventExistAsync(request.EventId))
            validation.AddValidation("There's no event using the requested Id");

        else if (await IsEventFullAsync(request.EventId))
            validation.AddValidation("This event is full.");
        #endregion

        #region Email
        if (string.IsNullOrWhiteSpace(request.Email))
            validation.AddValidation("Attendee's email cannot be empty.");

        else if (!MailAddress.TryCreate(request.Email, out _))
            validation.AddValidation("Attendee's email is not valid.");

        else if (await _repository.IsEmailInUseAsync(request.Email))
            validation.AddValidation("Attendee's email is already in use.");
        #endregion

        return validation;
    }

    private async Task<bool> IsEventFullAsync(Guid eventId)
    {
        var eventDto = await _eventsService.GetByIdAsync(eventId);
        var attendeesInEvent = await _repository.CountAttendeesByEventIdAsync(eventId);

        return attendeesInEvent == eventDto.MaximumAttendees;
    }
    #endregion
}
