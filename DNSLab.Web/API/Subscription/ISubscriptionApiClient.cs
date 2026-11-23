using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Subscription.DTOs;

namespace DNSLab.Web.API.Subscription;

public interface ISubscriptionApiClient
{
    Task<Result<List<SectionResponse>>> GetPlansAsync(CancellationToken cancellationToken = default);
    Task<Result<List<SubscriptionResponse>>> GetSubscribesAsync(CancellationToken cancellationToken = default);
    Task<Result<List<SubscriptionResponse>>> GetAllSubscribesAsync(CancellationToken cancellationToken = default);
    Task<Result> CheckSbscriptionFeatureAsync(FeatureRequest request, CancellationToken cancellationToken = default);
    Task<Result<InvoiceCreationResponse>> PurchaseSubscribeAsync(PurchaseSubscriptionRequest request, CancellationToken cancellationToken = default);
}
