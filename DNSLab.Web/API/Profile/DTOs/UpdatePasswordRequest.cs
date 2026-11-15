namespace DNSLab.Web.API.Profile.DTOs;

public record UpdatePasswordRequest(
    string Current,
    string New);
