using learning_ms.Web.Domain.Exceptions;

namespace learning_ms.Web.Application.Exceptions.UnAuthorizedException;

public sealed class UnAuthorizedException : DomainException
{
  private const int HttpStatusCode = 401;
  private const string DefaultTitle = "Unauthorized";

  public UnAuthorizedException()
    : base(DefaultTitle, "Authentication is required. Please log in to continue.", HttpStatusCode)
  { }

  public UnAuthorizedException(string message)
    : base(DefaultTitle, message, HttpStatusCode) { }
}
