using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.DNS.DTOs.Common;

namespace DNSLab.Web.API.DNS;

public interface IDNSApiClient
{
    Task<Result> AddRecordAsync(
        AddRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result> UpdateRecordAsync(
       UpdateRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result> DeleteRecordAsync(
       DeleteRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result<List<ZoneAndRecordsResponse>>> GetZonesAndRecordsAsync(
        CancellationToken cancellationToken = default);
}
