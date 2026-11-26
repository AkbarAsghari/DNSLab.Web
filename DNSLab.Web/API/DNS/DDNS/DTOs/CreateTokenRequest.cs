namespace DNSLab.Web.API.DNS.DDNS.DTOs;

public record CreateTokenRequest(
    string Name,
    List<Guid> RecordIds);
