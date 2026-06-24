using Domain.Exceptions;

namespace learning_ms.Web.Application.Exceptions.ForbiddenAccessException;

public sealed class ForbiddenAccessException : DomainException
{
  private const int HttpStatusCode = 403;
  private const string DefaultTitle = "Access Denied";

  public ForbiddenAccessException()
    : base(DefaultTitle, "You do not have permission to perform this action.", HttpStatusCode)
  {
  }

  public ForbiddenAccessException(string message)
    : base(DefaultTitle, message, HttpStatusCode)
  {
  }
  public ForbiddenAccessException(string action, string requiredRole)
    : base(DefaultTitle, $"Access denied. '{action}' requires the '{requiredRole}' role.", HttpStatusCode)
  {
  }
}
