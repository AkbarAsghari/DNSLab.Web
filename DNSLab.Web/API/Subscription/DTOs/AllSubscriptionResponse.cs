using DNSLab.Web.API.Profile.DTOs;
using DNSLab.Web.API.Subscription.Enums;

namespace DNSLab.Web.API.Subscription.DTOs;


public record AllSubscriptionResponse(
    DateTime StartDate,
    DateTime EndDate,
    long TotalPrice,
    SubscriptionStatus Status,
    DateTime CreateDate,
    DurationResponse Duration,
    SectionResponse Section,
    PlanResponse Plan,
    UserBasicResponse User);
