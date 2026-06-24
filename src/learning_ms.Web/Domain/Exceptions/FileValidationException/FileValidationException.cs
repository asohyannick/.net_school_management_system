namespace learning_ms.Web.Domain.Exceptions.FileValidationException;

public sealed class FileValidationException : Exception
{
    public FileValidationException(string message) : base(message) { }
}