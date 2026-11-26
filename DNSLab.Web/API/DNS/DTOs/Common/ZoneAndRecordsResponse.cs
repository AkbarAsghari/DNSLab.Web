using DNSLab.Web.API.Zone.DTOs;

namespace DNSLab.Web.API.DNS.DTOs.Common;

public record ZoneAndRecordsResponse(
    ZoneResponse Zone,
    List<RecordResponse> Records);
