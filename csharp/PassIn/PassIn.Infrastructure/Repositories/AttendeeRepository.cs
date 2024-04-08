
using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Models;
using PassIn.Infrastructure;

public class AttendeeRepository : IAttendeeRepository
{
  private readonly PassInDbContext _dbContext;

  public AttendeeRepository()
  {
    _dbContext = new PassInDbContext();
  }

  public void Add(Attendee attendee)
  {
    _dbContext.Attendees.Add(attendee);
    _dbContext.SaveChanges();
  }

  public long CountByEventId(Guid eventId)
  {
    return _dbContext.Attendees.Count(attendee => attendee.Event_Id.Equals(eventId));
  }

  public bool ExistsById(Guid id)
  {
    return _dbContext.Attendees.Any(attendee => attendee.Id == id);
  }

  public bool ExistsByEmailAndEventId(string email, Guid eventId)
  {
    return _dbContext.Attendees
      .Any(attendee => attendee.Email.Equals(email) && attendee.Event_Id.Equals(eventId));
  }

  public Attendee? FindByIdIncludesEventAndCheckIn(Guid id)
  {
    return _dbContext.Attendees.Include(at => at.Event).Include(at => at.CheckIn)
      .FirstOrDefault(at => at.Id == id);
  }
}
