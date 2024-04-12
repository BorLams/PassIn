using PassIn.Domain.Entities;

namespace PassIn.Infrastructure.Repositories.Interfaces;
public interface ICheckInsRepository
{
    Task<CheckIn> CheckInAsync(CheckIn checkIn);
    Task<IEnumerable<CheckIn>> GetAllAsync();
    Task<CheckIn> GetCheckInByIdAsync(Guid id);
}
