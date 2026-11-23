namespace DNSLab.Web.API.Subscription.DTOs;

public record PlanFullResponse(
    int Id,
    string Name,
    int BasePrice,
    List<DiscountResponse> Discounts,
    List<PlanFeatureResponse> Features);
