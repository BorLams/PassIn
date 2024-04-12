using PassIn.Application.Data.Dtos.CheckIn.Response;
using PassIn.Application.Data.Dtos.Event.Response;

namespace PassIn.Application.Data.Dtos.Attendee.Response;
public class ResponseAttendeeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid EventId { get; set; }

    public ResponseEventDto Event { get; set; }
    public ResponseCheckInDto CheckIn { get; set; }
}
