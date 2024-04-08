using PassIn.Communication.Responses;
using PassIn.Domain.Models;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Attendees.DoCheckIn;

public class DoAttendeeCheckInUseCase : IDoAttendeeCheckInUseCase
{
  private readonly ICheckInRepository _checkInRepository;
  private readonly IAttendeeRepository _attendeeRepository;

  public DoAttendeeCheckInUseCase(
    ICheckInRepository checkInRepository,
    IAttendeeRepository attendeeRepository)
  {
    _checkInRepository = checkInRepository;
    _attendeeRepository = attendeeRepository;
  }

  public ResponseRegisteredJson Execute(Guid attendeeId)
  {
    Validate(attendeeId);

    var entity = new CheckIn
    {
      Attendee_Id = attendeeId,
      Created_at = DateTime.UtcNow
    };
    _checkInRepository.Add(entity);

    return new ResponseRegisteredJson
    {
      Id = entity.Id
    };
  }

  private void Validate(Guid attendeeId)
  {
    var existAttendee = _attendeeRepository.ExistsById(attendeeId);
    if (existAttendee == false)
      throw new NotFoundException("The attendee with this Id was not found.");

    var existCheckIn = _checkInRepository.ExistsByAttendeeId(attendeeId);
    if (existCheckIn)
      throw new ConflictException("Attendee can not do checking twice in the same event.");
  }
}
