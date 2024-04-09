using PassIn.Application.Data.Dtos.Event.Response;

namespace PassIn.Application.Data.Dtos.Attendee.Response;
public class ResponseAttendeeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CheckedInAt { get; set; }

    public Guid EventId { get; set; }
    public ResponseEventDto Event { get; set; }
}
