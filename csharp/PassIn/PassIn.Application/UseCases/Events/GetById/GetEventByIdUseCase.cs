using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase : IGetEventByIdUseCase
{
  private readonly PassInDbContext _dbContext;

  public GetEventByIdUseCase()
  {
    _dbContext = new PassInDbContext();
  }

  public ResponseEventJson Execute(Guid id)
  {
    var entity = _dbContext.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id == id);
    
    if (entity is null) 
      throw new NotFoundException("An event with this id don't exist.");
    
    return new ResponseEventJson
    {
      Id = entity.Id,
      Title = entity.Title,
      Details = entity.Details,
      MaximumAttendees = entity.Maximum_Attendees,
      AttendeesAmount = entity.Attendees.Count()
    };
  }
}
