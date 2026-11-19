using DNSLab.Web.API.Billing.Enums;

namespace DNSLab.Web.API.Billing.DTOs;

public record InvoiceRequest(
    long Amount,
    long DiscountAmount,
    string CouponCode,
    bool UseWallet,
    Guid ItemId,
    InvoiceType InvoiceType);