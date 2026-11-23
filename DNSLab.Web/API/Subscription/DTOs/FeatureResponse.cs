using DNSLab.Domain.Plan.Enums;

namespace DNSLab.Web.API.Subscription.DTOs;

public record FeatureResponse(
    string Name,
    string Description,
    FeatureType Type,
    int Order,
    FeatureUnit Unit);