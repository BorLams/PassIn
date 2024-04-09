using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;

namespace PassIn.Infrastructure.Contexts;

public class PassInDbContext : DbContext
{
    public PassInDbContext(DbContextOptions<PassInDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
}
