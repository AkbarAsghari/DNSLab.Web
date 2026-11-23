namespace DNSLab.Application.Coupon.DTOs;

public record ValidateCouponRequest(
    string Code,
    long TotalAmount);
