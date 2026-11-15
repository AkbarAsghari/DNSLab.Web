using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Profile.DTOs;

namespace DNSLab.Web.API.Profile;

public class ProfileApiClient(IApiClient apiClient) : IProfileApiClient
{
    public Task<Result> DeactivateAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync(
           "api/profile/deactivate",
           cancellationToken: cancellationToken);
    }

    public Task<Result<List<UserProfileResponse>>> GetAllProfilesAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<UserProfileResponse>>(
           "api/profile/getAllProfiles",
           cancellationToken: cancellationToken);
    }

    public Task<Result<UserProfileResponse>> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateEmailAsync(UpdateEmailRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> UpdateMobileAsync(UpdateMobileRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdatePasswordAsync(UpdatePasswordRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateProfileAsync(UpdateProfileRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateUsernameAsync(UpdateUsernameRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
