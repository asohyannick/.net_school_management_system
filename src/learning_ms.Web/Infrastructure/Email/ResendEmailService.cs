using learning_ms.Web.Application.Interface.IEmailService;
using learning_ms.Web.Infrastructure.Email.Templates;
using Resend;
namespace learning_ms.Web.Infrastructure.Email;
public sealed class ResendEmailService : IEmailService
{
    private readonly IResend _resend;
    private readonly ResendSettings _settings;
    private readonly ILogger<ResendEmailService> _logger;
    public ResendEmailService(
        IResend resend,
        ResendSettings settings,
        ILogger<ResendEmailService> logger)
    {
        _resend = resend;
        _settings = settings;
        _logger = logger;
    }
    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default)
    {
        var message = new EmailMessage
        {
            From = $"{_settings.SenderName} <{_settings.SenderEmail}>",
            Subject = subject,
            HtmlBody = htmlBody
        };
        message.To.Add(toEmail);

        await SendAsync(message, cancellationToken);
    }
    public async Task SendBulkEmailAsync(
        IEnumerable<string> toEmails,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default)
    {
        var message = new EmailMessage
        {
            From = $"{_settings.SenderName} <{_settings.SenderEmail}>",
            Subject = subject,
            HtmlBody = htmlBody
        };

        foreach (var email in toEmails)
        {
            message.To.Add(email);
        }

        await SendAsync(message, cancellationToken);
    }
    public async Task SendWelcomeEmailAsync(
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.Welcome(firstName, lastName);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendOtpVerificationEmailAsync(
        string email,
        string firstName,
        string otpCode,
        int expiryMinutes,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.OtpVerification(firstName, otpCode, expiryMinutes);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendOtpResendEmailAsync(
        string email,
        string firstName,
        string otpCode,
        int expiryMinutes,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.OtpResend(firstName, otpCode, expiryMinutes);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendMagicLinkEmailAsync(
        string email,
        string firstName,
        string magicLinkUrl,
        int expiryMinutes,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.MagicLink(firstName, magicLinkUrl, expiryMinutes);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendMagicLinkResendEmailAsync(
        string email,
        string firstName,
        string magicLinkUrl,
        int expiryMinutes,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.MagicLinkResend(firstName, magicLinkUrl, expiryMinutes);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendForgotPasswordEmailAsync(
        string email,
        string firstName,
        string resetUrl,
        int expiryMinutes,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.ForgotPassword(firstName, resetUrl, expiryMinutes);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendAccountActivationEmailAsync(
        string email,
        string firstName,
        string activationUrl,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.AccountActivation(firstName, activationUrl);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendAccountDeletionEmailAsync(
        string email,
        string firstName,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.AccountDeletion(firstName);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendCoursePurchaseEmailAsync(
        string email,
        string firstName,
        string courseName,
        decimal amount,
        string currency,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.CoursePurchase(firstName, courseName, amount, currency);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendPromotionalDiscountEmailAsync(
        string email,
        string firstName,
        string discountCode,
        string discountDescription,
        DateTime expiryDateUtc,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.PromotionalDiscount(firstName, discountCode, discountDescription, expiryDateUtc);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendAccountBlockedEmailAsync(
        string email,
        string firstName,
        string reason,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.AccountBlocked(firstName, reason);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendAccountUnblockedEmailAsync(
        string email,
        string firstName,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.AccountUnblocked(firstName);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendStaffWelcomeEmailAsync(
        string email,
        string firstName,
        string lastName,
        string role,
        CancellationToken cancellationToken = default)
    {
        var (subject, html) = EmailTemplates.StaffWelcome(firstName, lastName, role);
        await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendAdmissionGrantedEmailAsync(
            string email,
            string firstName,
            string programName,
            string academicYear,
            string portalUrl,
            CancellationToken cancellationToken = default)
    {
            var (subject, html) = EmailTemplates.AdmissionGranted(firstName, programName, academicYear, portalUrl);
            await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendAdmissionRejectedEmailAsync(
      string email,
      string firstName,
      string programName,
      string reason,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.AdmissionRejected(firstName, programName, reason);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendTimetableCreatedEmailAsync(
      string email,
      string firstName,
      string termName,
      string timetableUrl,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.TimetableCreated(firstName, termName, timetableUrl);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
     
    public async Task SendBookBorrowedEmailAsync(
      string email,
      string firstName,
      string bookTitle,
      DateTime dueDateUtc,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.BookBorrowed(firstName, bookTitle, dueDateUtc);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendExamPreparationEmailAsync(
      string email,
      string firstName,
      string examName,
      DateTime examDateUtc,
      string instructions,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.ExamPreparation(firstName, examName, examDateUtc, instructions);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
    
    public async Task SendExamPassedEmailAsync(
      string email,
      string firstName,
      string examName,
      string grade,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.ExamPassed(firstName, examName, grade);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
     
    public async Task SendQuizPassedEmailAsync(
      string email,
      string firstName,
      string quizName,
      string score,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.QuizPassed(firstName, quizName, score);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendTutorAccountActivationEmailAsync(
      string email,
      string firstName,
      string activationUrl,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.TutorAccountActivation(firstName, activationUrl);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendTutorAccountBlockedEmailAsync(
      string email,
      string firstName,
      string reason,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.TutorAccountBlocked(firstName, reason);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
    public async Task SendStudentAnnouncementEmailAsync(
      IEnumerable<string> toEmails,
      string title,
      string message,
      string? ctaLabel = null,
      string? ctaUrl = null,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.StudentAnnouncement(title, message, ctaLabel, ctaUrl);
      await SendBulkEmailAsync(toEmails, subject, html, cancellationToken);
    }
    public async Task SendTutorAnnouncementEmailAsync(
      IEnumerable<string> toEmails,
      string title,
      string message,
      string? ctaLabel = null,
      string? ctaUrl = null,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.TutorAnnouncement(title, message, ctaLabel, ctaUrl);
      await SendBulkEmailAsync(toEmails, subject, html, cancellationToken);
    }
    public async Task SendAccommodationProvidedEmailAsync(
      string email,
      string firstName,
      string hostelName,
      string roomNumber,
      DateTime checkInDateUtc,
      CancellationToken cancellationToken = default)
    {
      var (subject, html) = EmailTemplates.AccommodationProvided(firstName, hostelName, roomNumber, checkInDateUtc);
      await SendEmailAsync(email, subject, html, cancellationToken);
    }
    private async Task SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _resend.EmailSendAsync(message, cancellationToken);
            _logger.LogInformation(
                "Email sent via Resend. Subject: {Subject}, Resend Id: {ResendId}",
                message.Subject, response.Content);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send email via Resend. Subject: {Subject}, Recipients: {Recipients}",
                message.Subject, string.Join(", ", message.To));
            throw;
        }
    }
}
