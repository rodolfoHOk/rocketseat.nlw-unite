using PassIn.Communication.Responses;

namespace PassIn.Application.UseCases.Attendees.DoCheckIn;

public interface IDoAttendeeCheckInUseCase
{
  ResponseRegisteredJson Execute(Guid attendeeId);
}
