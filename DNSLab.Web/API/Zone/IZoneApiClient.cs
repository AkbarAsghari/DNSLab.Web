using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Zone.DTOs;

namespace DNSLab.Web.API.Zone;

public interface IZoneApiClient
{
    Task<Result<Guid>> CreateZoneAsync(
        CreateZoneRequest request,
        CancellationToken cancellationToken = default);

    Task<Result> DeleteZoneAsync(
        ZoneIdRequest request,
        CancellationToken cancellationToken = default);

    Task<Result> DisableZoneAsync(
        ZoneIdRequest request,
        CancellationToken cancellationToken = default);

    Task<Result> EnableZoneAsync(
        ZoneIdRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<ZoneResponse>> GetZoneAsync(
        ZoneIdRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<List<ZoneResponse>>> GetZonesAsync(
        CancellationToken cancellationToken = default);
}
