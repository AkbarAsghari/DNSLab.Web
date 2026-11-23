using DNSLab.Application.Coupon.DTOs;
using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Coupon;

public class CouponApiClient(IApiClient apiClient) : ICouponApiClient
{
    const string _Prefix = "api/coupon/";

    public Task<Result<CouponValidationResponse>> ValidateAsync(ValidateCouponRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<ValidateCouponRequest, CouponValidationResponse>(
            $"{_Prefix}Validate",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }
}
