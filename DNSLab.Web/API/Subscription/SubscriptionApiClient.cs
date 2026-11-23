using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Subscription.DTOs;

namespace DNSLab.Web.API.Subscription;

public class SubscriptionApiClient(IApiClient apiClient) : ISubscriptionApiClient
{
    const string _Prefix = "api/subscription/";

    public Task<Result> CheckSbscriptionFeatureAsync(FeatureRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync(
            $"{_Prefix}CheckSbscription",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<List<SubscriptionResponse>>> GetAllSubscribesAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<SubscriptionResponse>>(
            $"{_Prefix}GetAllSubscribes",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<List<SectionResponse>>> GetPlansAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<SectionResponse>>(
           $"{_Prefix}GetPlans",
           new ApiRequestOptions { RequiresAuthentication = false },
           cancellationToken);
    }

    public Task<Result<List<SubscriptionResponse>>> GetSubscribesAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<SubscriptionResponse>>(
            $"{_Prefix}GetSubscribes",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<InvoiceCreationResponse>> PurchaseSubscribeAsync(PurchaseSubscriptionRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<PurchaseSubscriptionRequest, InvoiceCreationResponse>(
            $"{_Prefix}PurchaseSubscribe",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }
}
