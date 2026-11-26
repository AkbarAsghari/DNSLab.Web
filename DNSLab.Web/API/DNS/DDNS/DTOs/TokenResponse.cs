namespace DNSLab.Web.API.DNS.DDNS.DTOs;

public record TokenResponse(
    Guid TokenId,
    string Name,
    string Key);
