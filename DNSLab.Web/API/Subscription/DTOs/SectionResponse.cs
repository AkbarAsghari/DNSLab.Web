namespace DNSLab.Web.API.Subscription.DTOs;

public record SectionResponse(
    string Name,
    string Description,
    List<PlanFullResponse> Plans);