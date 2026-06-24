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
        string studentEmail,
        string studentFullName,
        CancellationToken cancellationToken = default);
}