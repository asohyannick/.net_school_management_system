namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateVerifyFirebaseTokenRequestDto
{
  public string VerifyFirebaseIdToken { get; set; } = string.Empty;
};
