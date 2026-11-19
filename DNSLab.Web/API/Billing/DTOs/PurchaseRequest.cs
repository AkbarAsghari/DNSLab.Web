namespace DNSLab.Web.API.Billing.DTOs;

public record PurchaseRequest(
    bool UseWallet,
    string CouponCode);
