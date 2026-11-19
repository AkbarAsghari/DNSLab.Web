
using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Authentication.DTOs;
using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Authentication;

public class AuthenticationApiClient(IApiClient apiClient) : IAuthenticationApiClient
{
    const string _Prefix = "api/authenticate/";

    public Task<Result<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<AuthenticateRequest, AuthenticateResponse>(
            $"{_Prefix}Authenticate",
            request,
            cancellationToken: cancellationToken);
    }

    public Task<Result<AuthenticateResponse>> ConfirmOtpAuthenticationAsync(ConfirmOtpRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<ConfirmOtpRequest, AuthenticateResponse>(
            $"{_Prefix}ConfirmOtpAuthentication",
            request,
            cancellationToken: cancellationToken);
    }

    public Task<Result<string>> ResendOtpAsync(ResendOtpRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync<ResendOtpRequest, string>(
            $"{_Prefix}ResendOtp",
            request,
            cancellationToken: cancellationToken);
    }

    public Task<Result<string>> StartOtpAuthenticationAsync(StartOtpAuthenticationRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<StartOtpAuthenticationRequest, string>(
            $"{_Prefix}StartOtpAuthentication",
            request,
            cancellationToken: cancellationToken);
    }
}
