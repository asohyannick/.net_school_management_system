using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace learning_ms.Web.Presentation.Filters.TagDescriptionsDocumentFilter;
public class TagDescriptionsDocumentFilter : IDocumentFilter
{
  public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
  {
    swaggerDoc.Tags = new List<OpenApiTag>
    {
      new()
      {
        Name = "Authentication and Authorization of Users",
        Description = "Handles user authentication and authorization including registration, OTP verification, login, logout, session management, and administrator account moderation."
      },
      new ()
      {
        Name = "Student Profile Management",
        Description = "Manages student profile records, including profile picture uploads processed asynchronously in the background via Hangfire and stored in MinIO."
      }
    };
  }
}
