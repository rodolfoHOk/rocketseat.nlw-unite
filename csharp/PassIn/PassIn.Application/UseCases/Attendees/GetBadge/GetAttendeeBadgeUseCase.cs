using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Attendees.GetBadge;

public class GetAttendeeBadgeUseCase : IGetAttendeeBadgeUseCase
{
  private readonly IAttendeeRepository _attendeeRepository;

  public GetAttendeeBadgeUseCase(IAttendeeRepository attendeeRepository)
  {
    _attendeeRepository = attendeeRepository;
  }

  public ResponseAttendeeBadgeJson Execute(Guid attendeeId)
  {
    var entity = _attendeeRepository.FindByIdIncludesEventAndCheckIn(attendeeId) ??
      throw new NotFoundException("An attendee with this id does not exist.");
    
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
