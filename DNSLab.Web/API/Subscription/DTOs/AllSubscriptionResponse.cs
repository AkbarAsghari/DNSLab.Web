using DNSLab.Application.Profile.DTOs;
using DNSLab.Domain.Subscription.Enums;

namespace DNSLab.Web.API.Subscription.DTOs;


public record AllSubscriptionResponse(
    DateTime StartDate,
    DateTime EndDate,
    long TotalPrice,
    SubscriptionStatus Status,
    DurationResponse Duration,
    UserBasicResponse User);
