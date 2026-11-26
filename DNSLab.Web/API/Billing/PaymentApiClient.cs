using DNSLab.Application.Coupon.DTOs;
using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Billing;

public class PaymentApiClient(IApiClient apiClient) : IPaymentApiClient
{
    const string _Prefix = "api/payment/";

    public Task<Result<PaginationResponse<AllPaymentResponse>>> GetAllPaymentsAsync(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PaginationRequest, PaginationResponse<AllPaymentResponse>>(
            $"{_Prefix}GetAllPayments",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<PaginationResponse<PaymentResponse>>> GetUserPaymentsAsync(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PaginationRequest, PaginationResponse<PaymentResponse>>(
            $"{_Prefix}GetUserPayments",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result> VerifyPaymentAsync(TrackIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync(
            $"{_Prefix}VerifyPayment",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }
}
