namespace DNSLab.Web.API.DNS.DDNS.DTOs;

public record UpdateTokenRequest(
    Guid TokenId,
    string Name,
    List<Guid> RecordIds);
