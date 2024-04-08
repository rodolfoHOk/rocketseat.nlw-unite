using PassIn.Domain.Models;

public interface IAttendeeRepository
{
  void Add(Attendee attendee);

  long CountByEventId(Guid eventId);

  bool ExistsById(Guid id);

  bool ExistsByEmailAndEventId(string email, Guid eventId);

  Attendee? FindByIdIncludesEventAndCheckIn(Guid id);
}
