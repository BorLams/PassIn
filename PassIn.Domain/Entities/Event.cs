using System.ComponentModel.DataAnnotations;

namespace PassIn.Domain.Entities;

public class Event
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public string Slug { get; set; }
    public int MaximumAttendees { get; set; }

    public virtual List<Attendee> Attendees { get; set; }
}
