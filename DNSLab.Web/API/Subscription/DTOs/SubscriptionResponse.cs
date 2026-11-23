using DNSLab.Web.API.Subscription.Enums;

namespace DNSLab.Web.API.Subscription.DTOs;

public record SubscriptionResponse(
    DateTime StartDate,
    DateTime EndDate,
    long TotalPrice,
    SubscriptionStatus Status,
    DateTime CreateDate,
    SectionResponse Section,
    PlanResponse Plan,
    DurationResponse Duration);