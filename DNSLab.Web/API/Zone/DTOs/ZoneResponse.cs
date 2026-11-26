using DNSLab.Web.API.Zone.Enums;

namespace DNSLab.Web.API.Zone.DTOs;

public record ZoneResponse(
    Guid Id,
    string Name,
    bool Disable,
    bool IsPublic,
    ZoneStatus Status,
    DateTime LastCheck,
    DateTime? ExpirationDate,
    DateTime CreateDate);
