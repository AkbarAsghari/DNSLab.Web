using DNSLab.Web.API.Billing.Enums;

namespace DNSLab.Web.API.Billing.DTOs;

public record InvoiceResponse(
    Guid InvoiceId,
    int Number,
    long TotalAmount,
    long DiscountAmount,
    long CouponDiscountAmount,
    long GatewayAmount,
    bool UseWalletAmount,
    DateTime? ExpireAt,
    Guid ItemId,
    InvoiceType Type,
    InvoiceStatus Status);
