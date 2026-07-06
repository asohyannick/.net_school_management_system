using learning_ms.Web.Application.Interface.IEmailService;
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
      string studentEmail,
      string studentFullName,
      CancellationToken cancellationToken = default)
  {
    var htmlBody = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;">
                <h2>Welcome, {studentFullName}!</h2>
                <p>Your enrollment was successful. You now have access to your student portal.</p>
                <p>If you have any questions, feel free to reach out to your administration office.</p>
                <p style="color: #777; font-size: 12px; margin-top: 24px;">
                    This is an automated message from {_settings.SenderName}.
                </p>
            </div>
            """;

    await SendEmailAsync(
        studentEmail,
        $"Welcome to {_settings.SenderName}!",
        htmlBody,
        cancellationToken);
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
