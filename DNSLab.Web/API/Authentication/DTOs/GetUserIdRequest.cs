namespace DNSLab.Web.API.Authentication.DTOs;

public record GetUserIdRequest(
    string Username,
    string Password);
