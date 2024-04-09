namespace PassIn.Application.Data.Dtos.Attendee.Request;
public class RequestRegisterAttendeeDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid EventId { get; set; }
}
