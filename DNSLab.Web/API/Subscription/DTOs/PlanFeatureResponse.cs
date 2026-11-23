namespace DNSLab.Web.API.Subscription.DTOs;

public record PlanFeatureResponse(
    bool Supported,
    int? FeatureValue,
    FeatureResponse Feature);
