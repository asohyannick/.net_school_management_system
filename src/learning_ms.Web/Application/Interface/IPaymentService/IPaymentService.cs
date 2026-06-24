namespace learning_ms.Web.Application.Interface.IPaymentService;
public interface IPaymentService
{

    Task<PaymentIntentResult> CreatePaymentIntentAsync(
        decimal amountInMajorUnits,
        string? currency,
        IDictionary<string, string>? metadata,
        CancellationToken cancellationToken = default);
}

public sealed record PaymentIntentResult(
    string PaymentIntentId,
    string ClientSecret,
    string PublishableKey
  );