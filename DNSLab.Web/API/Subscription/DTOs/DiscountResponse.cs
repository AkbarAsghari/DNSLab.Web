namespace DNSLab.Web.API.Subscription.DTOs;

public record DiscountResponse(
    int Id,
    int DiscountRate,
    DurationResponse Duration);