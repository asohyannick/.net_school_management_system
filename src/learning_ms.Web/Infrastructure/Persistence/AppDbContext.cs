using learning_ms.Web.Domain.Entities.StudentProfile;
using learning_ms.Web.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
namespace learning_ms.Web.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<StudentProfile> StudentProfiles =>
        Set<StudentProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
