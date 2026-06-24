namespace learning_ms.Web.Infrastructure.Email;
public sealed class ResendSettings
{
    public const string SectionName = "ResendSettings";
    public string ApiKey { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;
}