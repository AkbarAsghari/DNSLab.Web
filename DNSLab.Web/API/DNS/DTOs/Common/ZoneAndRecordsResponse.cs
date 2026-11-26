namespace DNSLab.Web.API.DNS.DTOs.Common;

public record ZoneAndRecordsResponse(
    Guid ZoneId,
    string Name,
    List<RecordResponse> Records);
