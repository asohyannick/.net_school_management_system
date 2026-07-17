using learning_ms.Web.Domain.Entities.User;
namespace learning_ms.Web.Infrastructure.Persistence.configurations.UserConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");
    builder.HasKey(u => u.Id);

    builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
    builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();
    builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
    builder.HasIndex(u => u.Email).IsUnique();

    builder.Property(u => u.Password).IsRequired();
    builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(50);
    builder.Property(u => u.ForgotPassword).HasMaxLength(512);
    
    builder.Property(u => u.OTPCode).HasMaxLength(6);
    builder.Property(u => u.ResendOTPCode).HasMaxLength(6);

    builder.Property(u => u.RefreshToken).HasMaxLength(512);
    builder.Property(u => u.AccessToken).HasMaxLength(2048);
  }
}
