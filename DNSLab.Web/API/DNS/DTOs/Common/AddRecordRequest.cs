using DNSLab.Web.API.DNS.Enums;
using System.Text.Json;

namespace DNSLab.Web.API.DNS.DTOs.Common;

public record AddRecordRequest(
    Guid ZoneId,
    string Name,
    int Ttl,
    string Comment,
    JsonElement Data,
    RecordType Type);