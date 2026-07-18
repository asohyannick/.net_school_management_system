namespace learning_ms.Web.Application.Interface.IPasswordHasher;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    bool NeedsRehash(string hashedPassword);
}
