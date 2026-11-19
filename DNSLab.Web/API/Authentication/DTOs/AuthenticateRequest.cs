namespace DNSLab.Web.API.Authentication.DTOs;

public record AuthenticateRequest(
    string Identifier,
    string Password);