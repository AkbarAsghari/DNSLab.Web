namespace DNSLab.Application.Coupon.DTOs;

public record CouponValidationResponse(
    Guid Id,
    long DiscountAmount);
