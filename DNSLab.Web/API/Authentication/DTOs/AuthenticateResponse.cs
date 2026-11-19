namespace DNSLab.Web.API.Authentication.DTOs;

public record AuthenticateResponse(
    string Token,
    DateTime TokenExpiryTime);