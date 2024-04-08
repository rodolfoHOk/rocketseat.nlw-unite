using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Models;
using PassIn.Infrastructure;

public class EventRepository : IEventRepository
{
  private readonly PassInDbContext _dbContext;

  public EventRepository()
  {
    _dbContext = new PassInDbContext();
  }

  public void Add(Event eventModel)
  {
    _dbContext.Events.Add(eventModel);
    _dbContext.SaveChanges();
  }

  public Event? FindById(Guid id)
  {
    return _dbContext.Events.Find(id);
  }

    public Event? FindByIdIncludesAttendees(Guid id)
  {
    return _dbContext.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id == id);
  }

  public Event? FindByIdIncludesAttendeesWithCheckIns(Guid id)
  {
    return _dbContext.Events.Include(ev => ev.Attendees)
      .ThenInclude(attendee => attendee.CheckIn)
      .FirstOrDefault(ev => ev.Id == id);
  }
}
