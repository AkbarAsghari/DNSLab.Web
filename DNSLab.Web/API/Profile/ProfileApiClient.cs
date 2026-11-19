using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Profile.DTOs;

namespace DNSLab.Web.API.Profile;

public class ProfileApiClient(IApiClient apiClient) : IProfileApiClient
{
    const string _Prefix = "api/profile/";

    public Task<Result> DeactivateAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync(
            $"{_Prefix}Deactivate",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<UserProfileResponse>>> GetAllProfilesAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<UserProfileResponse>>(
            $"{_Prefix}GetAllProfiles",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<UserProfileResponse>> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<UserProfileResponse>(
            $"{_Prefix}GetProfile",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> IsMobileApprovedAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync(
            $"{_Prefix}GetProfile",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> UpdateEmailAsync(UpdateEmailRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}UpdateEmail",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<string>> UpdateMobileAsync(UpdateMobileRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync<UpdateMobileRequest ,string>(
            $"{_Prefix}UpdateMobile",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> UpdatePasswordAsync(UpdatePasswordRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}UpdatePassword",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> UpdateProfileAsync(UpdateProfileRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}UpdateProfile",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> UpdateUsernameAsync(UpdateUsernameRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}UpdateUsername",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }
}
