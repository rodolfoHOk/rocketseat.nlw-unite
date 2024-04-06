using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetBadge;

public class GetAttendeeBadgeUseCase
{
  private readonly PassInDbContext _dbContext;

  public GetAttendeeBadgeUseCase()
  {
    _dbContext = new PassInDbContext();
  }

  public ResponseAttendeeBadgeJson Execute(Guid attendeeId)
  {
    var entity = _dbContext.Attendees.Include(at => at.Event).Include(at => at.CheckIn)
      .FirstOrDefault(at => at.Id == attendeeId);
    if (entity == null) {
      throw new NotFoundException("An attendee with this id does not exist.");
    }

    return new ResponseAttendeeBadgeJson
    {
      badge = new AttendeeBadgeDTO {
        Id = entity.Id,
        Name = entity.Name,
        Email = entity.Email,
        EventTitle = entity.Event?.Title,
        CheckInURL = "http://localhost:5210/api/attendees/" + entity.Id + "/check-in"
      }
    };
  }
}
