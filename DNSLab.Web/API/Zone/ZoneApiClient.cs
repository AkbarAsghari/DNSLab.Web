using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Ticket.DTOs;
using DNSLab.Web.API.Zone.DTOs;

namespace DNSLab.Web.API.Zone;

public class ZoneApiClient(IApiClient apiClient) : IZoneApiClient
{
    const string _Prefix = "api/zone/";

    public Task<Result<Guid>> CreateZoneAsync(CreateZoneRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<CreateZoneRequest, Guid>(
            $"{_Prefix}CreateZone",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> DeleteZoneAsync(ZoneIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync(
            $"{_Prefix}DeleteZone",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> DisableZoneAsync(ZoneIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}DisableZone",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> EnableZoneAsync(ZoneIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}EnableZone",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<ZoneResponse>> GetZoneAsync(ZoneIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<ZoneIdRequest, ZoneResponse>(
            $"{_Prefix}GetZone",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<ZoneResponse>>> GetZonesAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<ZoneResponse>>(
            $"{_Prefix}GetZones",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }
}
