using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Models;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{
  public DbSet<Event> Events { get; set; }
  public DbSet<Attendee> Attendees { get; set; }
  public DbSet<CheckIn> CheckIns { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=/var/home/rodolfo/Documentos/ti/rocketseat/nlw-unite/csharp/PassIn/PassIn.Infrastructure/PassInDb.db");
  }
}
