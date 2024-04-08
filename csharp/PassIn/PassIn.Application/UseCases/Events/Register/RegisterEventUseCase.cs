using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Models;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase : IRegisterEventUseCase
{
  private readonly IEventRepository _eventRepository;

  public RegisterEventUseCase(IEventRepository eventRepository)
  {
    _eventRepository = eventRepository;
  }

  public ResponseRegisteredEventJson Execute(RequestEventJson request)
  {
    Validate(request);

    var entity = new Event
    {   
      Title = request.Title,
      Details = request.Details,
      Maximum_Attendees = request.MaximumAttendees,
      Slug = request.Title.ToLower().Replace(" ", "-"),
    };
    _eventRepository.Add(entity);

    return new ResponseRegisteredEventJson {
      Id = entity.Id
    };
  }

  public void Validate(RequestEventJson request)
  {
    if (request.MaximumAttendees <= 0)
    {
      throw new ErrorOnValidationException("The maximum attendees is invalid.");
    }

    if (string.IsNullOrWhiteSpace(request.Title))
    {
      throw new ErrorOnValidationException("The title is invalid.");
    }

    if (string.IsNullOrWhiteSpace(request.Details))
    {
      throw new ErrorOnValidationException("The details is invalid.");
    }
  }
}
