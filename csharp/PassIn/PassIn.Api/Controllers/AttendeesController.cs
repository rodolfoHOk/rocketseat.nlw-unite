using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.DoCheckIn;
using PassIn.Application.UseCases.Attendees.GetBadge;
using PassIn.Communication.Responses;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
  [HttpGet]
  [Route("{id}/badge")]
  [ProducesResponseType(typeof(ResponseAttendeeBadgeJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult GetBadge([FromRoute]Guid id)
  {
    var useCase = new GetAttendeeBadgeUseCase();
    var response = useCase.Execute(id);
    return Ok(response);
  }

  [HttpPost]
  [Route("{id}/check-in")]
  [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
  public IActionResult RegisterCheckIn([FromRoute]Guid id)
  {
    var useCase = new DoAttendeeCheckInUseCase();
    var response = useCase.Execute(id);
    return Created(string.Empty, response);
  }
}
