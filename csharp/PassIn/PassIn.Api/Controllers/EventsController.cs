using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Register;
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
}
