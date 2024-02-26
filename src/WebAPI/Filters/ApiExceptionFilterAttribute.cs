using Application.common.Exceptions;
using Application.common.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

using NotFoundException = Application.common.Exceptions.NotFoundException;

namespace WebAPI.Filters;

/// <summary>
///   Represents an API exception filter attribute that handles various exceptions.
/// </summary>
public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
  private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

  /// <summary>
  ///   Represents an API exception filter attribute that handles various exceptions.
  /// </summary>
  public ApiExceptionFilterAttribute()
  {
    // Register known exception types and handlers.
    _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
    {
        { typeof(ValidationException), HandleValidationException },
        { typeof(NotFoundException), HandleNotFoundException },
        { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
        { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
        { typeof(DbUpdateException), HandleDbUpdateException }
    };
  }

  public override void OnException(ExceptionContext context)
  {
    HandleException(context);

    base.OnException(context);
  }

  private void HandleException(ExceptionContext context)
  {
    var type = context.Exception.GetType();

    if (_exceptionHandlers.TryGetValue(type, out var handler))
    {
      handler.Invoke(context);

      return;
    }

    if (!context.ModelState.IsValid)
    {
      HandleInvalidModelStateException(context);

      return;
    }

    HandleUnknownException(context);
  }

  private void HandleValidationException(ExceptionContext context)
  {
    var exception = (ValidationException)context.Exception;

    var problemDetails = new ValidationProblemDetails(exception.Errors)
    {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
    };

    var response =
        ApiResponse<ValidationProblemDetails>.Failure("Validation error occurred",
                                                      StatusCodes.Status400BadRequest,
                                                      problemDetails);

    context.Result = new BadRequestObjectResult(response);

    context.ExceptionHandled = true;
  }

  private void HandleInvalidModelStateException(ExceptionContext context)
  {
    var problemDetails = new ValidationProblemDetails(context.ModelState)
    {
        Status = StatusCodes.Status400BadRequest,
        Title = "Validation Error",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
    };

    var response =
        ApiResponse<ValidationProblemDetails>.Failure("Invalid model state",
                                                      StatusCodes.Status400BadRequest,
                                                      problemDetails);

    context.Result = new BadRequestObjectResult(response);

    context.ExceptionHandled = true;
  }

  private void HandleNotFoundException(ExceptionContext context)
  {
    var exception = (NotFoundException)context.Exception;

    var problemDetails = new ProblemDetails
    {
        Status = StatusCodes.Status404NotFound,
        Title = "Not Found",
        Detail = exception.Message,
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
    };

    var response =
        ApiResponse<ProblemDetails>.Failure("Resource not found",
                                            StatusCodes.Status404NotFound,
                                            problemDetails);

    context.Result = new NotFoundObjectResult(response);

    context.ExceptionHandled = true;
  }

  private void HandleUnauthorizedAccessException(ExceptionContext context)
  {
    var problemDetails = new ProblemDetails
    {
        Status = StatusCodes.Status401Unauthorized,
        Title = "Unauthorized",
        Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
    };

    var response = ApiResponse<ProblemDetails>.Failure("Unauthorized access",
                                                       StatusCodes.Status401Unauthorized,
                                                       problemDetails);

    context.Result = new ObjectResult(response) { StatusCode = StatusCodes
        .Status401Unauthorized };

    context.ExceptionHandled = true;
  }

  private void HandleForbiddenAccessException(ExceptionContext context)
  {
    var problemDetails = new ProblemDetails
    {
        Status = StatusCodes.Status403Forbidden,
        Title = "Forbidden",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
    };

    var response =
        ApiResponse<ProblemDetails>.Failure("Access forbidden",
                                            StatusCodes.Status403Forbidden,
                                            problemDetails);

    context.Result = new ObjectResult(response) { StatusCode = StatusCodes.Status403Forbidden };

    context.ExceptionHandled = true;
  }

  private void HandleDbUpdateException(ExceptionContext context)
  {
    var exception = context.Exception as DbUpdateException;

    var problemDetails = new ProblemDetails
    {
        Status = StatusCodes.Status500InternalServerError,
        Title = "Database Update Error",
        Detail = exception?.Message,
        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
    };

    var response =
        ApiResponse<ProblemDetails>
            .Failure("An error occurred while updating the database",
                     StatusCodes.Status500InternalServerError,
                     problemDetails);

    context.Result = new ObjectResult(response)
    {
        StatusCode = StatusCodes.Status500InternalServerError
    };

    context.ExceptionHandled = true;
  }

  private void HandleUnknownException(ExceptionContext context)
  {
    var problemDetails = new ProblemDetails
    {
        Status = StatusCodes.Status500InternalServerError,
        Title = "An unknown error occurred",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
    };

    var response = ApiResponse<ProblemDetails>.Failure(context.Exception?.Message ?? "An error occurred",
                                                       StatusCodes
                                                           .Status500InternalServerError,
                                                       problemDetails);

    context.Result =
        new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

    context.ExceptionHandled = true;
  }
}
