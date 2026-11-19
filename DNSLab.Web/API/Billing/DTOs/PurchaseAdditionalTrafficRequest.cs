namespace DNSLab.Web.API.Billing.DTOs;

public record PurchaseAdditionalTrafficRequest(
    int Traffic,
    bool UseWallet,
    string CouponCode)
    : PurchaseRequest(UseWallet, CouponCode);
