
using DNSLab.Web.API.Authentication.DTOs;
using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Authentication;

public interface IAuthenticationApiClient
{
    Task<Result<AuthenticateResponse>> AuthenticateAsync(
        AuthenticateRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<string>> StartOtpAuthenticationAsync(
       StartOtpAuthenticationRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<AuthenticateResponse>> ConfirmOtpAuthenticationAsync(
        ConfirmOtpRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<string>> ResendOtpAsync(
       ResendOtpRequest request,
        CancellationToken cancellationToken = default);
}
