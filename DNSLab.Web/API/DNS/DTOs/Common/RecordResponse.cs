using DNSLab.Web.API.DNS.Enums;
using System.Text.Json;

namespace DNSLab.Web.API.DNS.DTOs.Common;

public record RecordResponse(
    Guid RecordId,
    string Name,
    uint TTL,
    string Comment,
    bool Disable,
    DateTime? LastModified,
    JsonElement Data,
    RecordType Type);