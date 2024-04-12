using PassIn.Application.Data.Dtos.CheckIn.Request;
using PassIn.Application.Data.Dtos.CheckIn.Response;
using PassIn.Application.Validations;

namespace PassIn.Application.Services.Interfaces;
public interface ICheckInsService
{
    Task<(ValidationResult, ResponseCheckInDto)> CheckInAsync(RequestCheckInDto request);
    Task<IEnumerable<ResponseCheckInDto>> GetAllAsync();
}
