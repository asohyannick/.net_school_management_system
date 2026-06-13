using Microsoft.AspNetCore.Mvc;
namespace learning_ms.Web.Presentation.Controllers.test;
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() =>
        Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
}
