using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Domain.Entities;
public class Attendee
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(Event))]
    public Guid EventId { get; set; }
    public virtual Event Event { get; set; }
}
