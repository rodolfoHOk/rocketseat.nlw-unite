using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase : IGetEventByIdUseCase
{
  private readonly IEventRepository _eventRepository;

  public GetEventByIdUseCase(IEventRepository eventRepository)
  {
    _eventRepository = eventRepository;
  }

  public ResponseEventJson Execute(Guid id)
  {
    var entity = _eventRepository.FindByIdIncludesAttendees(id) ??
      throw new NotFoundException("An event with this id don't exist.");
    
    return new ResponseEventJson
    {
      Id = entity.Id,
      Title = entity.Title,
      Details = entity.Details,
      MaximumAttendees = entity.Maximum_Attendees,
      AttendeesAmount = entity.Attendees.Count()
    };
  }
}
