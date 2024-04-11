using PassIn.Application.Data.Dtos.Attendee.Response;

namespace PassIn.Application.Data.Dtos.CheckIn.Response;
public class ResponseCheckInDto
{
    public Guid Id { get; set; }
    public DateTime CheckedInAt { get; set; }
    public Guid AttendeeId { get; set; }

    public virtual ResponseAttendeeDto Attendee { get; set; }
}
