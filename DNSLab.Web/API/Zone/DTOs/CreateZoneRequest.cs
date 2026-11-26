namespace DNSLab.Web.API.Zone.DTOs;

public record CreateZoneRequest(
    string Name,
    bool IsPublic = false);
