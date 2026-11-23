using DNSLab.Application.Subscription.Enums;

namespace DNSLab.Web.API.Subscription.DTOs;

public record FeatureRequest(
    FeatureEnum Feature,
    Guid? UserId = null);