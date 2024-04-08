using PassIn.Communication.Responses;

namespace PassIn.Application.UseCases.Attendees.GetBadge;

public interface IGetAttendeeBadgeUseCase
{
  ResponseAttendeeBadgeJson Execute(Guid attendeeId);
}
