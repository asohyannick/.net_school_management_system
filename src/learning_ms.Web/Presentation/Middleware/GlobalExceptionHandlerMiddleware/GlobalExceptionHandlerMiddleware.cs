using System.Net;
using System.Text.Json;
using learning_ms.Web.Application.Exceptions.BadRequestException;
using learning_ms.Web.Application.Exceptions.ForbiddenAccessException;
using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Exceptions.UnAuthorizedException;
using Microsoft.AspNetCore.Mvc;
namespace learning_ms.Web.Presentation.Middleware;
public sealed class GlobalExceptionHandlerMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
  };

  public GlobalExceptionHandlerMiddleware(
      RequestDelegate next,
      ILogger<GlobalExceptionHandlerMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }

  private async Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    var (statusCode, title, detail, validationErrors) = ResolveExceptionDetails(exception);

    if (statusCode >= 500)
      _logger.LogError(exception, "Unhandled server error: {Message}", exception.Message);
    else
      _logger.LogWarning(exception, "Client error [{StatusCode}]: {Message}", statusCode, exception.Message);

    var problemDetails = new ProblemDetails
    {
      Status = statusCode,
      Title = title,
      Detail = detail,
      Instance = context.Request.Path
    };

    if (validationErrors is not null)
      problemDetails.Extensions["validationErrors"] = validationErrors;

    context.Response.ContentType = "application/problem+json";
    context.Response.StatusCode = statusCode;

    await context.Response.WriteAsync(
        JsonSerializer.Serialize(problemDetails, JsonOptions));
  }

  private static (int StatusCode, string Title, string Detail, object? ValidationErrors) ResolveExceptionDetails(
      Exception exception)
  {
    return exception switch
    {
      BadRequestException ex => (
          ex.StatusCode,
          ex.Title,
          ex.Message,
          ex.ValidationErrors as object),

      NotFoundException ex => (
          ex.StatusCode,
          ex.Title,
          ex.Message,
          null),

      UnAuthorizedException ex => (
          ex.StatusCode,
          ex.Title,
          ex.Message,
          null),

      ForbiddenAccessException ex => (
          ex.StatusCode,
          ex.Title,
          ex.Message,
          null),

      InternalServerErrorException ex => (
          ex.StatusCode,
          ex.Title,
          ex.Message,
          null),

      _ => (
          (int)HttpStatusCode.InternalServerError,
          "Internal Server Error",
          "An unexpected error occurred. Please contact support if the issue persists.",
          null)
    };
  }
}
