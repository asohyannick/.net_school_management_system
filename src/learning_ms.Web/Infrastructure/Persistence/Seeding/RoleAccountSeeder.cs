using learning_ms.Web.Domain.Entities.User;
using learning_ms.Web.Domain.Enums.UserRole;
using learning_ms.Web.Infrastructure.ConfigurationExtensions.RoleSeedSettingsExtensions;
namespace learning_ms.Web.Infrastructure.Persistence.Seeding;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public static class RoleAccountSeeder
{
    public static async Task SeedAsync(
        AppDbContext dbContext,
        IPasswordHasher<User> passwordHasher,
        IOptions<RoleSeedSettingsExtensions> settingsOptions,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        var settings = settingsOptions.Value;

        var seedMap = new (UserRole Role, string? Email, string? Password)[]
        {
            (UserRole.SuperAdmin, settings.SuperAdminEmail, settings.SuperAdminPassword),
            (UserRole.HeadOfDepartment, settings.HeadOfDepartmentEmail, settings.HeadOfDepartmentPassword),
            (UserRole.AcademicDirector, settings.AcademicDirectorEmail, settings.AcademicDirectorPassword),
            (UserRole.VicePrincipal, settings.VicePrincipalEmail, settings.VicePrincipalPassword),
            (UserRole.Registrar, settings.RegistrarEmail, settings.RegistrarPassword),
            (UserRole.Bursar, settings.BursarEmail, settings.BursarPassword),
            (UserRole.Accountant, settings.AccountantEmail, settings.AccountantPassword),
            (UserRole.FinanceManager, settings.FinanceManagerEmail, settings.FinanceManagerPassword),
            (UserRole.HrManager, settings.HrManagerEmail, settings.HrManagerPassword),
            (UserRole.HrOfficer, settings.HrOfficerEmail, settings.HrOfficerPassword),
            (UserRole.PayrollOfficer, settings.PayrollOfficerEmail, settings.PayrollOfficerPassword),
            (UserRole.AdmissionsOfficer, settings.AdmissionsOfficerEmail, settings.AdmissionsOfficerPassword),
            (UserRole.ExamOfficer, settings.ExamOfficerEmail, settings.ExamOfficerPassword),
            (UserRole.Librarian, settings.LibrarianEmail, settings.LibrarianPassword),
            (UserRole.AssistantLibrarian, settings.AssistantLibrarianEmail, settings.AssistantLibrarianPassword),
            (UserRole.LabManager, settings.LabManagerEmail, settings.LabManagerPassword),
            (UserRole.LabTechnician, settings.LabTechnicianEmail, settings.LabTechnicianPassword),
            (UserRole.IctManager, settings.IctManagerEmail, settings.IctManagerPassword),
            (UserRole.ItSupport, settings.ItSupportEmail, settings.ItSupportPassword),
            (UserRole.SystemAdmin, settings.SystemAdminEmail, settings.SystemAdminPassword),
            (UserRole.NetworkAdmin, settings.NetworkAdminEmail, settings.NetworkAdminPassword),
            (UserRole.SecurityOfficer, settings.SecurityOfficerEmail, settings.SecurityOfficerPassword),
            (UserRole.ProcurementOfficer, settings.ProcurementOfficerEmail, settings.ProcurementOfficerPassword),
            (UserRole.StoreKeeper, settings.StoreKeeperEmail, settings.StoreKeeperPassword),
            (UserRole.HostelManager, settings.HostelManagerEmail, settings.HostelManagerPassword),
            (UserRole.HostelWarden, settings.HostelWardenEmail, settings.HostelWardenPassword),
            (UserRole.TransportManager, settings.TransportManagerEmail, settings.TransportManagerPassword),
            (UserRole.Driver, settings.DriverEmail, settings.DriverPassword),
            (UserRole.Nurse, settings.NurseEmail, settings.NursePassword),
            (UserRole.SchoolDoctor, settings.SchoolDoctorEmail, settings.SchoolDoctorPassword),
            (UserRole.Counselor, settings.CounselorEmail, settings.CounselorPassword),
            (UserRole.Receptionist, settings.ReceptionistEmail, settings.ReceptionistPassword),
            (UserRole.Secretary, settings.SecretaryEmail, settings.SecretaryPassword),
            (UserRole.PublicRelationsOfficer, settings.PublicRelationsOfficerEmail, settings.PublicRelationsOfficerPassword),
            (UserRole.QualityAssuranceManager, settings.QualityAssuranceManagerEmail, settings.QualityAssuranceManagerPassword),
            (UserRole.QualityAssuranceOfficer, settings.QualityAssuranceOfficerEmail, settings.QualityAssuranceOfficerPassword),
            (UserRole.ComplianceOfficer, settings.ComplianceOfficerEmail, settings.ComplianceOfficerPassword),
            (UserRole.LegalOfficer, settings.LegalOfficerEmail, settings.LegalOfficerPassword),
            (UserRole.MarketingManager, settings.MarketingManagerEmail, settings.MarketingManagerPassword),
            (UserRole.EventCoordinator, settings.EventCoordinatorEmail, settings.EventCoordinatorPassword),
            (UserRole.SportsDirector, settings.SportsDirectorEmail, settings.SportsDirectorPassword),
            (UserRole.SportsCoach, settings.SportsCoachEmail, settings.SportsCoachPassword),
            (UserRole.DeanOfStudies, settings.DeanOfStudiesEmail, settings.DeanOfStudiesPassword),
            (UserRole.DisciplineMaster, settings.DisciplineMasterEmail, settings.DisciplineMasterPassword),
            (UserRole.ClassCoordinator, settings.ClassCoordinatorEmail, settings.ClassCoordinatorPassword),
            (UserRole.TimetableOfficer, settings.TimetableOfficerEmail, settings.TimetableOfficerPassword),
            (UserRole.CurriculumManager, settings.CurriculumManagerEmail, settings.CurriculumManagerPassword),
            (UserRole.AlumniCoordinator, settings.AlumniCoordinatorEmail, settings.AlumniCoordinatorPassword),
            (UserRole.ResearchCoordinator, settings.ResearchCoordinatorEmail, settings.ResearchCoordinatorPassword),
            (UserRole.TrainingCoordinator, settings.TrainingCoordinatorEmail, settings.TrainingCoordinatorPassword),
            (UserRole.FacilitiesManager, settings.FacilitiesManagerEmail, settings.FacilitiesManagerPassword),
            (UserRole.MaintenanceSupervisor, settings.MaintenanceSupervisorEmail, settings.MaintenanceSupervisorPassword),
        };

        var seededCount = 0;

        foreach (var (role, email, password) in seedMap)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                logger.LogWarning(
                    "Skipping seed for role {Role} — missing email or password in configuration.", role);
                continue;
            }

            var exists = await dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
            if (exists)
            {
                continue; // already seeded — idempotent, safe to run every startup
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = role.ToString(),
                LastName = "Staff",
                Email = email,
                Role = role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            user.Password = passwordHasher.HashPassword(user, password);

            dbContext.Users.Add(user);
            seededCount++;
        }

        if (seededCount > 0)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            logger.LogInformation("✅ Seeded {Count} role-based staff account(s).", seededCount);
        }
    }
}
