namespace DNSLab.Web.API.DNS.DDNS.DTOs;

public record NicRequest(
    string HostName,
    string NewIp,
    string Username,
    string Password);
