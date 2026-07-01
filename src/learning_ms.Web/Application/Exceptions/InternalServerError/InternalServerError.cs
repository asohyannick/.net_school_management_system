using learning_ms.Web.Domain.Exceptions;

namespace learning_ms.Web.Application.Exceptions.InternalServerError;

public sealed class InternalServerErrorException : DomainException
{
  private const int HttpStatusCode = 500;
  private const string DefaultTitle = "Internal Server Error";

  public InternalServerErrorException()
    : base(DefaultTitle, "An unexpected error occurred. Please try again later.", HttpStatusCode)
  { }

  public InternalServerErrorException(string message)
    : base(DefaultTitle, message, HttpStatusCode) { }

  public InternalServerErrorException(string message, Exception innerException)
    : base(DefaultTitle, message, HttpStatusCode) { }
}
