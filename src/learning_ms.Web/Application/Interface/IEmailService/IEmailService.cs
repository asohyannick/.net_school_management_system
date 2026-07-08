namespace learning_ms.Web.Application.Interface.IEmailService;
public interface IEmailService
{
    Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default);

    Task SendBulkEmailAsync(
        IEnumerable<string> toEmails,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default);

    Task SendWelcomeEmailAsync(
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);
    Task SendOtpVerificationEmailAsync(
        string email,
        string firstName,
        string otpCode,
        int expiryMinutes,
        CancellationToken cancellationToken = default);
    Task SendOtpResendEmailAsync(
        string email,
        string firstName,
        string otpCode,
        int expiryMinutes,
        CancellationToken cancellationToken = default);
    Task SendMagicLinkEmailAsync(
        string email,
        string firstName,
        string magicLinkUrl,
        int expiryMinutes,
        CancellationToken cancellationToken = default);
    Task SendMagicLinkResendEmailAsync(
        string email,
        string firstName,
        string magicLinkUrl,
        int expiryMinutes,
        CancellationToken cancellationToken = default);
    Task SendForgotPasswordEmailAsync(
        string email,
        string firstName,
        string resetUrl,
        int expiryMinutes,
        CancellationToken cancellationToken = default);
    Task SendAccountActivationEmailAsync(
        string email,
        string firstName,
        string activationUrl,
        CancellationToken cancellationToken = default);
    Task SendAccountDeletionEmailAsync(
        string email,
        string firstName,
        CancellationToken cancellationToken = default);
    Task SendCoursePurchaseEmailAsync(
        string email,
        string firstName,
        string courseName,
        decimal amount,
        string currency,
        CancellationToken cancellationToken = default);
    Task SendPromotionalDiscountEmailAsync(
        string email,
        string firstName,
        string discountCode,
        string discountDescription,
        DateTime expiryDateUtc,
        CancellationToken cancellationToken = default);
    Task SendAccountBlockedEmailAsync(
        string email,
        string firstName,
        string reason,
        CancellationToken cancellationToken = default);
    Task SendAccountUnblockedEmailAsync(
        string email,
        string firstName,
        CancellationToken cancellationToken = default);
    Task SendStaffWelcomeEmailAsync(
        string email,
        string firstName,
        string lastName,
        string role,
        CancellationToken cancellationToken = default);
    Task SendAdmissionGrantedEmailAsync(
        string email,
        string firstName,
        string programName,
        string academicYear,
        string portalUrl,
        CancellationToken cancellationToken = default);
    Task SendAdmissionRejectedEmailAsync(
        string email,
        string firstName,
        string programName,
        string reason,
        CancellationToken cancellationToken = default);
    Task SendTimetableCreatedEmailAsync(
        string email,
        string firstName,
        string termName,
        string timetableUrl,
        CancellationToken cancellationToken = default);
    Task SendBookBorrowedEmailAsync(
        string email,
        string firstName,
        string bookTitle,
        DateTime dueDateUtc,
        CancellationToken cancellationToken = default);
    Task SendExamPreparationEmailAsync(
        string email,
        string firstName,
        string examName,
        DateTime examDateUtc,
        string instructions,
        CancellationToken cancellationToken = default);
    Task SendExamPassedEmailAsync(
        string email,
        string firstName,
        string examName,
        string grade,
        CancellationToken cancellationToken = default);
    Task SendQuizPassedEmailAsync(
        string email,
        string firstName,
        string quizName,
        string score,
        CancellationToken cancellationToken = default);
    Task SendTutorAccountActivationEmailAsync(
        string email,
        string firstName,
        string activationUrl,
        CancellationToken cancellationToken = default);
    Task SendTutorAccountBlockedEmailAsync(
        string email,
        string firstName,
        string reason,
        CancellationToken cancellationToken = default);
 
    Task SendStudentAnnouncementEmailAsync(
        IEnumerable<string> toEmails,
        string title,
        string message,
        string? ctaLabel = null,
        string? ctaUrl = null,
        CancellationToken cancellationToken = default);
 
    Task SendTutorAnnouncementEmailAsync(
        IEnumerable<string> toEmails,
        string title,
        string message,
        string? ctaLabel = null,
        string? ctaUrl = null,
        CancellationToken cancellationToken = default);
 
    Task SendAccommodationProvidedEmailAsync(
        string email,
        string firstName,
        string hostelName,
        string roomNumber,
        DateTime checkInDateUtc,
        CancellationToken cancellationToken = default);
}
