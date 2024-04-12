using System.ComponentModel.DataAnnotations;

namespace PassIn.Domain.Entities;
public class Attendee
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid EventId { get; set; }
    public virtual Event Event { get; set; }

    public virtual CheckIn CheckIn { get; set; }
}
