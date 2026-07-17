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
        Description = "Handles creation, retrieval, updating, and management of student profile information."
      }
    };
  }
}
