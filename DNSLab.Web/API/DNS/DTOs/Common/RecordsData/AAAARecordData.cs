using DNSLab.Web.API.DNS.Interfaces;

namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record AAAARecordData(
    string IPv6Address) : IRecordData;