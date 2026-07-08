namespace learning_ms.Web.Infrastructure.ApiResponse;
public sealed class ApiResponse<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }
    public List<string>? Errors { get; init; }
    public int StatusCode { get; init; }

    public DateTime TimestampUtc { get; init; } = DateTime.UtcNow;
    private ApiResponse() { }
    
    public static ApiResponse<T> SuccessResponse(T data, string message = "Request completed successfully", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = statusCode
        };
    }
    public static ApiResponse<T> SuccessResponse(string message = "Request completed successfully", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = default,
            StatusCode = statusCode
        };
    }
    public static ApiResponse<T> FailureResponse(string message, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = new List<string> { message },
            StatusCode = statusCode
        };
    }
    public static ApiResponse<T> FailureResponse(string message, List<string> errors, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }
}

public sealed class ApiResponse
{
    public static ApiResponse<object> SuccessResponse(string message = "Request completed successfully", int statusCode = 200)
        => ApiResponse<object>.SuccessResponse(message, statusCode);

    public static ApiResponse<object> FailureResponse(string message, int statusCode = 400)
        => ApiResponse<object>.FailureResponse(message, statusCode);

    public static ApiResponse<object> FailureResponse(string message, List<string> errors, int statusCode = 400)
        => ApiResponse<object>.FailureResponse(message, errors, statusCode);
}
