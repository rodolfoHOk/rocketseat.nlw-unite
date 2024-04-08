using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Application.UseCases.Events.Register;
public interface IRegisterEventUseCase
{
  public ResponseRegisteredEventJson Execute(RequestEventJson request);
}
