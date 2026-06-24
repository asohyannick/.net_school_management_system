using Domain.Exceptions;
namespace learning_ms.Web.Application.Exceptions.BadRequestException;

public sealed class BadRequestException : DomainException
{
  private const int HttpStatusCode = 400;
  private const string DefaultTitle = "Bad Request";

  public IReadOnlyDictionary<string, string[]>? ValidationErrors { get; }

  public BadRequestException(string message)
      : base(DefaultTitle, message, HttpStatusCode)
  {
  }
  public BadRequestException(string message, IDictionary<string, string[]> validationErrors)
      : base(DefaultTitle, message, HttpStatusCode)
  {
    ValidationErrors = validationErrors.AsReadOnly();
  }
}
