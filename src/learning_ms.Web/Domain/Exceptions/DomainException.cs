namespace learning_ms.Web.Domain.Exceptions;
public abstract class DomainException : Exception
{
  public int StatusCode { get; }
  public string Title { get; }

  protected DomainException(string title, string message, int statusCode)
      : base(message)
  {
    Title = title;
    StatusCode = statusCode;
  }
}
