using learning_ms.Web.Application.Interface.IPasswordHasher;
namespace learning_ms.Web.Infrastructure.Auth.BcryptPasswordHasher;
public class BcryptPasswordHasher : IPasswordHasher
{
  private const int WorkFactor = 12;
  public string HashPassword(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);
  }
  public bool VerifyPassword(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
  public bool NeedsRehash(string hashedPassword)
  {
    return BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, WorkFactor);
  }
}
