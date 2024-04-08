using System.Net.Mail;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Models;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase : IRegisterAttendeeOnEventUseCase
{
  private readonly IAttendeeRepository _attendeeRepository;
  private readonly IEventRepository _eventRepository;

  public RegisterAttendeeOnEventUseCase(IAttendeeRepository attendeeRepository, IEventRepository eventRepository)
  {
    _attendeeRepository = attendeeRepository;
    _eventRepository = eventRepository;
  }

  public ResponseRegisteredAttendeeJson Execute(Guid eventId, RequestAttendeeJson request)
  {
    Validate(eventId, request);

    var entity = new Attendee
    {   
      Email = request.Email,
      Name = request.Name,
      Event_Id = eventId,
      Created_At = DateTime.UtcNow,
    };
    _attendeeRepository.Add(entity);

    return new ResponseRegisteredAttendeeJson
    {
      Id = entity.Id,
    };
  }

  private void Validate(Guid eventId, RequestAttendeeJson request)
  {
    var eventEntity = _eventRepository.FindById(eventId) ?? 
      throw new NotFoundException("An event with this id does not exist.");
        
    if (string.IsNullOrWhiteSpace(request.Name))
      throw new ErrorOnValidationException("The name is invalid.");

    if (EmailIsValid(request.Email) == false)
      throw new ErrorOnValidationException("The e-mail is invalid.");

    var attendeeAlreadyRegistered = _attendeeRepository.ExistsByEmailAndEventId(request.Email, eventId);
    if (attendeeAlreadyRegistered)
      throw new ConflictException("You can not register twice on the event.");

    var attendeesForEvent = _attendeeRepository.CountByEventId(eventId);
    if (attendeesForEvent >= eventEntity.Maximum_Attendees)
      throw new ErrorOnValidationException("There is no room for this event.");
  }

  private bool EmailIsValid(string email)
  {
    try
    {
      new MailAddress(email);
      return true;
    }
    catch
    {
      return false;
    }
  }
}
