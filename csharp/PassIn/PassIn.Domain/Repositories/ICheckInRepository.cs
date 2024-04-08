using PassIn.Domain.Models;

public interface ICheckInRepository
{
  void Add(CheckIn checkIn);

  bool ExistsByAttendeeId(Guid id);
}
