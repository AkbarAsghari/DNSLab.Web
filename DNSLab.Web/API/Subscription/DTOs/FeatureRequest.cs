using DNSLab.Web.API.Subscription.Enums;

namespace DNSLab.Web.API.Subscription.DTOs;

public record FeatureRequest(
    FeatureEnum Feature);