using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.DNS.DDNS.DTOs;
using DNSLab.Web.API.DNS.DTOs.Common;

namespace DNSLab.Web.API.DNS.DDNS;

public class DDNSApiClient(IApiClient apiClient) : IDDNSApiClient
{
    const string _Prefix = "api/ddns/";

    public Task<Result> AddRecordAsync(AddRecordRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync(
            $"{_Prefix}AddRecord",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result> CreateTokenAsync(CreateTokenRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync(
            $"{_Prefix}CreateToken",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result> DeleteRecordAsync(DeleteRecordRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync(
            $"{_Prefix}DeleteRecord",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result> DeleteTokenAsync(TokenIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync(
            $"{_Prefix}DeleteToken",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<List<ZoneAndRecordsResponse>>> GetTodayChangedZonesWithDDNSRecordsAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<ZoneAndRecordsResponse>>(
            $"{_Prefix}GetTodayChangedZonesWithDDNSRecords",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<TokenAndRecordsResponse>> GetTokenAsync(TokenIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<TokenIdRequest, TokenAndRecordsResponse>(
            $"{_Prefix}GetToken",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<List<TokenResponse>>> GetTokensAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<TokenResponse>>(
            $"{_Prefix}GetTokens",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<TokenScriptResponse>> GetTokenScriptAsync(TokenScriptRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<TokenScriptRequest, TokenScriptResponse>(
            $"{_Prefix}GetTokenScript",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<List<ZoneAndRecordsResponse>>> GetZonesAndARecordsAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<ZoneAndRecordsResponse>>(
            $"{_Prefix}GetZonesAndARecords",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<List<ZoneAndRecordsResponse>>> GetZonesAndRecordsAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<ZoneAndRecordsResponse>>(
            $"{_Prefix}GetZonesAndRecords",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result<TokenResponse>> RevokeTokenKeyAsync(TokenIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync<TokenIdRequest, TokenResponse>(
            $"{_Prefix}RevokeTokenKey",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result> UpdateRecordAsync(UpdateRecordRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}UpdateRecord",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }

    public Task<Result> UpdateTokenAsync(UpdateTokenRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}UpdateRecord",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken);
    }
}
