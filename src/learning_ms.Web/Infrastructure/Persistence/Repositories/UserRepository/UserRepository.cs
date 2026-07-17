using learning_ms.Web.Application.Interface.IUserRepository;
using learning_ms.Web.Domain.Entities.User;
namespace learning_ms.Web.Infrastructure.Persistence.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;
  public UserRepository(AppDbContext context) => _context = context;

  public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
    _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

  public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
    _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

  public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default) =>
    _context.Users.AnyAsync(u => u.Email == email, cancellationToken);

  public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
    await _context.Users.AddAsync(user, cancellationToken);

  public void Update(User user) => _context.Users.Update(user);

  public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
    _context.SaveChangesAsync(cancellationToken);
  public Task<User?> GetByOtpCodeAsync(string otpCode, CancellationToken cancellationToken = default) =>
    _context.Users.FirstOrDefaultAsync(u => u.OTPCode == otpCode, cancellationToken);
  public async Task<(List<User> Items, int TotalCount)> GetPagedAsync(
      int page, int perPage, CancellationToken cancellationToken = default)
  {
      var query = _context.Users.OrderByDescending(u => u.CreatedAt);
  
      var totalCount = await query.CountAsync(cancellationToken);
      var items = await query
          .Skip((page - 1) * perPage)
          .Take(perPage)
          .ToListAsync(cancellationToken);
  
      return (items, totalCount);
  }
  public void Remove(User user) => _context.Users.Remove(user);
  
}
