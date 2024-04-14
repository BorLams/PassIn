using AutoMapper;
using PassIn.Application.Data.Dtos.CheckIn.Request;
using PassIn.Application.Data.Dtos.CheckIn.Response;
using PassIn.Application.Services.Interfaces;
using PassIn.Application.Validations;
using PassIn.Domain.Entities;
using PassIn.Infrastructure.Repositories.Interfaces;

namespace PassIn.Application.Services;
public class CheckInsService : ICheckInsService
{
    #region Service Initialization
    private readonly ICheckInsRepository _repository;
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IMapper _mapper;

    public CheckInsService(ICheckInsRepository repository, IMapper mapper, IAttendeeRepository attendeeRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _attendeeRepository = attendeeRepository;
    }
    #endregion

    public async Task<IEnumerable<ResponseCheckInDto>> GetAllAsync()
        => _mapper.Map<IEnumerable<ResponseCheckInDto>>(await _repository.GetAllAsync());

    public async Task<(ValidationResult, ResponseCheckInDto)> CheckInAsync(RequestCheckInDto request)
    {
        var validation = Validate(request);
        ResponseCheckInDto response = null;

        if (validation.IsValid)
        {
            var checkIn = _mapper.Map<CheckIn>(request);

            response = _mapper.Map<ResponseCheckInDto>(await _repository.CheckInAsync(checkIn));
        }

        return (validation, response);
    }

    #region Private Methods
    private async ValidationResult Validate(RequestCheckInDto request)
    {
        var validation = new ValidationResult();

        if (Equals(request.AttendeeId, Guid.Empty))
            validation.AddError("Attendee's Id cannot be empty");

        else if (!await _attendeeRepository.DoesAttendeeExist(request.AttendeeId))
            validation.AddError("");

        return validation;
    }
    #endregion
}
