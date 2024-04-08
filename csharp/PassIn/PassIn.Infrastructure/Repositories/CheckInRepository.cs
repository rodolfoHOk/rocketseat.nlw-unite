using PassIn.Domain.Models;
using PassIn.Infrastructure;

public class CheckInRepository : ICheckInRepository
{
  private readonly PassInDbContext _dbContext;

  public CheckInRepository()
  {
    _dbContext = new PassInDbContext();
  }

  public void Add(CheckIn checkIn)
  {
    _dbContext.CheckIns.Add(checkIn);
    _dbContext.SaveChanges();
  }

  public bool ExistsByAttendeeId(Guid attendeeId)
  {
    return _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);
  }
}
