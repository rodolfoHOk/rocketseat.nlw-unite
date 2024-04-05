using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    var result = context.Exception is PassInException;
    if (result)
    {
      HandleProjectException(context);
    }
    else
    {
      ThrowUnknownError(context);
    }
  }

  private void HandleProjectException(ExceptionContext context)
  {
    switch (context.Exception)
    {
      case NotFoundException:
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
        break;
      case ErrorOnValidationException:
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
        break;
      default: 
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(context.Exception.Message));
        break;
    }
  }

  private void ThrowUnknownError(ExceptionContext context)
  {
    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Result = new ObjectResult(new ResponseErrorJson("Unknown error"));
  }
}
