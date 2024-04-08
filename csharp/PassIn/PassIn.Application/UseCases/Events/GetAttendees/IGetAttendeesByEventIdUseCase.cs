using PassIn.Communication.Responses;

namespace PassIn.Application.UseCases.Events.GetAttendees;

public interface IGetAttendeesByEventIdUseCase
{
  ResponseAttendeesListJson Execute(Guid eventId);
}
