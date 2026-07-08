namespace learning_ms.Web.Domain.Interfaces;
public interface IEmailSender
{
  Task SendEmailAsync(string toEmail, string from, string subject, string htmlBody);
}
