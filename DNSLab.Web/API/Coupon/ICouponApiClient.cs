using DNSLab.Application.Coupon.DTOs;
using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Coupon;

public interface ICouponApiClient
{
    Task<Result<CouponValidationResponse>> ValidateAsync(
        ValidateCouponRequest request,
        CancellationToken cancellationToken = default);
}
