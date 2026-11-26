using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.DNS.DDNS.DTOs;
using DNSLab.Web.API.DNS.DTOs.Common;

namespace DNSLab.Web.API.DNS.DDNS;

public interface IDDNSApiClient
{
    Task<Result> AddRecordAsync(
        AddRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result> UpdateRecordAsync(
        UpdateRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result> DeleteRecordAsync(
        DeleteRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result<List<ZoneAndRecordsResponse>>> GetZonesAndRecordsAsync(
        CancellationToken cancellationToken = default);

    Task<Result<List<ZoneAndRecordsResponse>>> GetTodayChangedZonesWithDDNSRecordsAsync(
        CancellationToken cancellationToken = default);

    Task<Result<List<ZoneAndRecordsResponse>>> GetZonesAndARecordsAsync(
        CancellationToken cancellationToken = default);


    // Tokens
    Task<Result> CreateTokenAsync(
        CreateTokenRequest request, CancellationToken cancellationToken = default);

    Task<Result> UpdateTokenAsync(
        UpdateTokenRequest request, CancellationToken cancellationToken = default);

    Task<Result> DeleteTokenAsync(
        TokenIdRequest request, CancellationToken cancellationToken = default);

    Task<Result<TokenResponse>> RevokeTokenKeyAsync(
        TokenIdRequest request, CancellationToken cancellationToken = default);

    Task<Result<List<TokenResponse>>> GetTokensAsync(
        CancellationToken cancellationToken = default);

    Task<Result<TokenAndRecordsResponse>> GetTokenAsync(
        TokenIdRequest request, CancellationToken cancellationToken = default);

    Task<Result<TokenScriptResponse>> GetTokenScriptAsync(
        TokenScriptRequest request, CancellationToken cancellationToken = default);
}
