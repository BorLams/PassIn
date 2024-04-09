using PassIn.Application.Data.Dtos.Attendee.Response;

namespace PassIn.Application.Data.Dtos.Event.Response;
public class ResponseEventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public int MaximumAttendees { get; set; }
    public int AttendeesAmount { get; set; }

    public List<ResponseAttendeeDto> Attendees { get; set; }
}
