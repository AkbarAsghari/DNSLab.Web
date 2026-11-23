using DNSLab.Domain.Identity.Entities;
using DNSLab.Domain.Plan.Entities;
using DNSLab.Domain.Subscription.Enums;

namespace DNSLab.Web.API.Subscription.DTOs;

public record SubscriptionResponse(
    DateTime StartDate,
    DateTime EndDate,
    long TotalPrice,
    SubscriptionStatus Status,
    DurationResponse Duration);