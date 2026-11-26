using DNSLab.Web.API.DNS.Enums;

namespace DNSLab.Web.API.DNS.DTOs.Common;

public record DeleteRecordRequest(
    Guid RecordId,
    RecordType Type);