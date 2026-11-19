using DNSLab.Web.API.Profile.Enums;

namespace DNSLab.Web.API.Profile.DTOs;

public record UserProfileResponse(
    Guid Id,
    string? Username,
    string? FirstName,
    string? LastName,
    string? Company,
    string? Mobile,
    bool IsMobileApproved,
    string? Email,
    bool IsEmailApproved,
    Role Role,
    bool Active,
    DateTime LastLoginTime,
    DateTime CreateDate,
    DateTime? UpdateDate);