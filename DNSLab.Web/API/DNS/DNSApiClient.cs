using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.DNS.DTOs.Common;

namespace DNSLab.Web.API.DNS;

public class DNSApiClient(IApiClient apiClient) : IDNSApiClient
{
    const string _Prefix = "api/dns/";

    public Task<Result> AddRecordAsync(AddRecordRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync(
            $"{_Prefix}AddRecord",
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

    public Task<Result<List<ZoneAndRecordsResponse>>> GetZonesAndRecordsAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<ZoneAndRecordsResponse>>(
            $"{_Prefix}GetZonesAndRecords",
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
}
