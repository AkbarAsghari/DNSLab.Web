using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Billing;

public interface IPaymentApiClient
{
    Task<Result> VerifyPaymentAsync(
        TrackIdRequest request, CancellationToken cancellationToken = default);

    Task<Result<PaginationResponse<PaymentResponse>>> GetUserPaymentsAsync(
        PaginationRequest request, CancellationToken cancellationToken = default);

    Task<Result<PaginationResponse<PaymentResponse>>> GetAllPaymentsAsync(
        PaginationRequest request, CancellationToken cancellationToken = default);
}
