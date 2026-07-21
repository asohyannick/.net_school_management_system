using learning_ms.Web.Domain.Entities.StudentProfile;
namespace learning_ms.Web.Infrastructure.Persistence.configurations.StudentProfileConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
  public void Configure(EntityTypeBuilder<StudentProfile> builder)
  {
    builder.HasKey(s => s.Id);

    builder.HasIndex(s => s.AdmissionNumber).IsUnique();
    builder.HasIndex(s => s.UserId);

    builder.Property(s => s.AdmissionNumber).HasMaxLength(50).IsRequired();
    builder.Property(s => s.FirstName).HasMaxLength(100).IsRequired();
    builder.Property(s => s.LastName).HasMaxLength(100).IsRequired();
    builder.Property(s => s.Email).HasMaxLength(256);
    builder.Property(s => s.Gender).HasConversion<string>().HasMaxLength(20);

    builder.Property(s => s.ProfilePictureUrl);
    builder.Property(s => s.Hobbies);

    builder.Property(s => s.PendingImageCount).HasDefaultValue(0);
  }
}
