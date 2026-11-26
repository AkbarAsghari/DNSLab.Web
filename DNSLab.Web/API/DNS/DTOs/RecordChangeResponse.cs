using DNSLab.Web.API.DNS.Enums;

namespace DNSLab.Web.API.DNS.DTOs;

public record RecordChangeResponse(
    string? PreviousData,
    string? CurrentData,
    RecordType RecordType,
    ChangeOperation Operation,
    DateTime CreateDate,
    bool IsProcessed);
