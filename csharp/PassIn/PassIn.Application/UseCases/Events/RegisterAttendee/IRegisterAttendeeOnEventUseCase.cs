using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public interface IRegisterAttendeeOnEventUseCase
{
  ResponseRegisteredAttendeeJson Execute(Guid eventId, RequestAttendeeJson request);
}
