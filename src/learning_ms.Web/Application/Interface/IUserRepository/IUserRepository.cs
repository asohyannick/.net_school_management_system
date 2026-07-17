using learning_ms.Web.Domain.Entities.User;
namespace learning_ms.Web.Application.Interface.IUserRepository;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    void Update(User user);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByOtpCodeAsync(string otpCode, CancellationToken cancellationToken = default);
    Task<(List<User> Items, int TotalCount)> GetPagedAsync(int page, int perPage, CancellationToken cancellationToken = default);
    void Remove(User user);
}
