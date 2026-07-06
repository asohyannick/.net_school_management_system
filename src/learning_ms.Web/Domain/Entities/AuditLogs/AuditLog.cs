using learning_ms.Web.Domain.Enums.AuditSeverities;
namespace learning_ms.Web.Domain.Entities.AuditLogs;

public class AuditLog
{
  public Guid Id { get; set; }

  public Guid? UserId { get; set; }

  public string UserName { get; set; } = default!;

  public string UserRole { get; set; } = default!;

  public string Action { get; set; } = default!;

  public string EntityName { get; set; } = default!;

  public Guid? EntityId { get; set; }

  public string? OldValues { get; set; }

  public string? NewValues { get; set; }

  public string? ChangedFields { get; set; }

  public string? IpAddress { get; set; }

  public string? UserAgent { get; set; }

  public string? Device { get; set; }

  public string? Location { get; set; }

  public string? RequestPath { get; set; }

  public string? HttpMethod { get; set; }

  public int? StatusCode { get; set; }

  public string? CorrelationId { get; set; }

  public AuditSeverity Severity { get; set; } = AuditSeverity.Info;

  public bool IsSuccess { get; set; }

  public string? ErrorMessage { get; set; }

  public DateTime Timestamp { get; set; }

  public long DurationMs { get; set; }

  public DateTime CreatedAt { get; set; }
}
