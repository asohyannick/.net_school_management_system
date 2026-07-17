namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateBlockUserRequestDto
{
   public string Reason { get; set;  } = string.Empty;
}
