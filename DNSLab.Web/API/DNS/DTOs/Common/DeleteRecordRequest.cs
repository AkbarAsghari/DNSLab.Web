using DNSLab.Domain.DNS.Enums;

namespace DNSLab.Web.API.DNS.DTOs.Common;

public record DeleteRecordRequest(
    Guid RecordId,
    RecordType Type);