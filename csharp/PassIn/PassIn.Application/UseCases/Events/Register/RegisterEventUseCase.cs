using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase : IRegisterEventUseCase
{
  private readonly PassInDbContext _dbContext;

  public RegisterEventUseCase()
  {
    _dbContext = new PassInDbContext();
  }

  public ResponseRegisteredEventJson Execute(RequestEventJson request)
  {
    Validate(request);

    var entity = new Infrastructure.Entities.Event
    {   
      Title = request.Title,
      Details = request.Details,
      Maximum_Attendees = request.MaximumAttendees,
      Slug = request.Title.ToLower().Replace(" ", "-"),
    };
    _dbContext.Events.Add(entity);
    _dbContext.SaveChanges();

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
