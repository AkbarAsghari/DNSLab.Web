namespace DNSLab.Web.API.DNS.DDNS.DTOs;

public record NicResponse(
    int HttpStatusCode,
    string Message);