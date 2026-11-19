namespace DNSLab.Web.API.Billing.DTOs;

public record PurchaseSubscriptionRequest(
    int PlanId,
    int DiscountId,
    bool UseWallet,
    string CouponCode)
    : PurchaseRequest(UseWallet, CouponCode);
