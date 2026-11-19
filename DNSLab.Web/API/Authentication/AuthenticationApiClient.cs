
using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Authentication.DTOs;
using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Authentication;

public class AuthenticationApiClient(IApiClient apiClient) : IAuthenticationApiClient
{
    const string _Prefix = "api/profile/";

    public Task<Result<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync<AuthenticateRequest, AuthenticateResponse>(
            $"{_Prefix}Authenticate",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<AuthenticateResponse>> ConfirmOtpAuthenticationAsync(ConfirmOtpRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync<ConfirmOtpRequest, AuthenticateResponse>(
            $"{_Prefix}ConfirmOtpAuthentication",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<string>> StartOtpAuthenticationAsync(StartOtpAuthenticationRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync<StartOtpAuthenticationRequest, string>(
            $"{_Prefix}StartOtpAuthentication",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }
}
