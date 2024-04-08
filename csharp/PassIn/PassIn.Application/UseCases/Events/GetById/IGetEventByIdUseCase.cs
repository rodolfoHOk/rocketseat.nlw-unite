using PassIn.Communication.Responses;

namespace PassIn.Application.UseCases.Events.GetById;

public interface IGetEventByIdUseCase
{
  public ResponseEventJson Execute(Guid id);
}
