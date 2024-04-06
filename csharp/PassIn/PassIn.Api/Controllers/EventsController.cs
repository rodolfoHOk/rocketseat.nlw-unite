using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetAttendees;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisteredEventJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public IActionResult Register([FromBody] RequestEventJson request)
  {
    var useCase = new RegisterEventUseCase();
    var response = useCase.Execute(request);
    var uri = "http://localhost:5210/api/events/" + response.Id;
    return Created(uri, response);
  }

  [HttpGet]
  [Route("{id}")]
  [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult GetById([FromRoute] Guid id)
  {
    var useCase = new GetEventByIdUseCase();
    var response = useCase.Execute(id);
    return Ok(response);
  }

  [HttpPost]
  [Route("{id}/attendees")]
  [ProducesResponseType(typeof(ResponseRegisteredAttendeeJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
  public IActionResult RegisterAttendee([FromRoute] Guid id, [FromBody] RequestAttendeeJson request)
  {
    var useCase = new RegisterAttendeeOnEventUseCase();
    var response = useCase.Execute(id, request);
    var uri = "http://localhost:5210/api/ateendees/" + response.Id + "/badge";
    return Created(uri, response);
  }

  [HttpGet]
  [Route("{id}/attendees")]
  [ProducesResponseType(typeof(ResponseAttendeesListJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult GetAttendees([FromRoute] Guid id)
  {
    var useCase = new GetAttendeesByEventIdUseCase();
    var response = useCase.Execute(id);
    return Ok(response);
  }
}
