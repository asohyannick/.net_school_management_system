using learning_ms.Web.Domain.Enums.PaymentCategory;
using learning_ms.Web.Domain.Enums.PaymentMethods;
using learning_ms.Web.Domain.Enums.PaymentProvider;
using learning_ms.Web.Domain.Enums.PaymentStatus;
using learning_ms.Web.Domain.Enums.PaymentType;

namespace learning_ms.Web.Domain.Entities.Payment;

public class Payment
{
  public Guid Id { get; set; }

  public string PaymentReference { get; set; } = default!;

  public string ExternalTransactionId { get; set; } = string.Empty;

  public decimal Amount { get; set; }

  public decimal TaxAmount { get; set; }

  public decimal DiscountAmount { get; set; }

  public decimal ProcessingFee { get; set; }

  public decimal TotalAmount { get; set; }

  public string Currency { get; set; } = "USD";

  public Guid? StudentId { get; set; }

  public Guid? ParentId { get; set; }

  public Guid? EmployeeId { get; set; }

  public PaymentType PaymentType { get; set; }

  public PaymentCategory PaymentCategory { get; set; }

  public PaymentMethod PaymentMethod { get; set; }

  public PaymentProvider PaymentProvider { get; set; }

  public PaymentStatus Status { get; set; }

  public string? InvoiceNumber { get; set; }

  public DateOnly? DueDate { get; set; }

  public DateTime? PaidAt { get; set; }

  public string? CheckoutSessionId { get; set; }

  public string? PaymentIntentId { get; set; }

  public string? CustomerId { get; set; }

  public string? PaymentUrl { get; set; }

  public string Description { get; set; } = string.Empty;

  public string? Notes { get; set; }

  public bool IsRefunded { get; set; }

  public decimal RefundedAmount { get; set; }

  public DateTime? RefundedAt { get; set; }

  public string? RefundReason { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public Guid? CreatedBy { get; set; }

  public Guid? UpdatedBy { get; set; }
}
