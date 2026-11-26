using DNSLab.Web.API.DNS.Interfaces;

namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record ARecordData(
    string IPv4Address) : IRecordData;
