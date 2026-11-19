using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Profile.DTOs;

namespace DNSLab.Web.API.Profile;

public interface IProfileApiClient
{
    Task<Result> UpdateProfileAsync(UpdateProfileRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateUsernameAsync(UpdateUsernameRequest request, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateMobileAsync(UpdateMobileRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateEmailAsync(UpdateEmailRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdatePasswordAsync(UpdatePasswordRequest request, CancellationToken cancellationToken = default);

    Task<Result<UserProfileResponse>> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<Result<List<UserProfileResponse>>> GetAllProfilesAsync(CancellationToken cancellationToken = default);
    Task<Result> IsMobileApprovedAsync(CancellationToken cancellationToken = default);

    Task<Result> DeactivateAsync(CancellationToken cancellationToken = default);
}
