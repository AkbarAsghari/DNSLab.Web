namespace DNSLab.Web.API.Profile.DTOs;

public record UpdateProfileRequest(
    string FirstName,
    string LastName,
    string Company);
