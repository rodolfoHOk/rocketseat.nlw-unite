using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.GetAttendees;

public class GetAttendeesByEventIdUseCase : IGetAttendeesByEventIdUseCase
{
  private readonly IEventRepository _eventRepository;

  public GetAttendeesByEventIdUseCase(IEventRepository eventRepository)
  {
    _eventRepository = eventRepository;
  }

  public ResponseAttendeesListJson Execute(Guid eventId)
  {
    var entity = _eventRepository.FindByIdIncludesAttendeesWithCheckIns(eventId) ??
      throw new NotFoundException("An event with this id does not exist.");
        
    return new ResponseAttendeesListJson
    {
      Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
      {
        Id = attendee.Id,
        Name = attendee.Name,
        Email = attendee.Email,
        CreatedAt = attendee.Created_At,
        CheckedInAt = attendee.CheckIn?.Created_at
      }).ToList(),
      Total = entity.Attendees.Count()
    };
  }
}
