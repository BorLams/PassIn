using System.ComponentModel.DataAnnotations;

namespace PassIn.Domain.Entities;
public class CheckIn
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CheckedInAt { get; set; }

    public Guid AttendeeId { get; set; }
    public virtual Attendee Attendee { get; set; }
}
