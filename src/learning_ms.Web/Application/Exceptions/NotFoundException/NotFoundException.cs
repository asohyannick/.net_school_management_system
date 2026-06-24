using Domain.Exceptions;
namespace learning_ms.Web.Application.Exceptions.NotFoundException;
public sealed class NotFoundException : DomainException
{
    private const int HttpStatusCode = 404;
    private const string DefaultTitle = "Resource Not Found";

    public NotFoundException(string message)
        : base(DefaultTitle, message, HttpStatusCode)
    {
    }
    public NotFoundException(string resourceName, object resourceId)
        : base(DefaultTitle, $"{resourceName} with id '{resourceId}' was not found.", HttpStatusCode)
    {
    }
}
