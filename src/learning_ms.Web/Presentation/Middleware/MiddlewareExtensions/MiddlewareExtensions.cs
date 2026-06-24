
namespace learning_ms.Web.Presentation.Middleware.MiddlewareExtensions;
public static class MiddlewareExtensions
{

  public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
  => app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}