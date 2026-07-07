namespace learning_ms.Web.Application.Common.DTOs.User;
public record CreateVerifyFirebaseTokenRequestDto
{
  public required string VerifyFirebaseIdToken { get; set; }    
};
