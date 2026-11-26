using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.DNS.DTOs;
using DNSLab.Web.API.DNS.DTOs.Common;
using DNSLab.Web.API.Zone.DTOs;

namespace DNSLab.Web.API.DNS;

public interface IDNSApiClient
{
    Task<Result> AddRecordAsync(
        AddRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result> UpdateRecordAsync(
       UpdateRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result> DeleteRecordAsync(
       DeleteRecordRequest request, CancellationToken cancellationToken = default);

    Task<Result<ZoneAndRecordsResponse>> GetZoneRecordsAsync(
        ZoneIdRequest request, CancellationToken cancellationToken = default);

    Task<Result<PaginationResponse<RecordChangeResponse>>> GetRecordChangeHistoryAsync(
        RecordChangeRequest request, CancellationToken cancellationToken = default);
}
