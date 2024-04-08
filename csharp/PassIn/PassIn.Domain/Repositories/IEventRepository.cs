using PassIn.Domain.Models;

public interface IEventRepository
{
  void Add(Event eventModel);

  Event? FindById(Guid id);
  
  Event? FindByIdIncludesAttendees(Guid id);

  Event? FindByIdIncludesAttendeesWithCheckIns(Guid id);
}
