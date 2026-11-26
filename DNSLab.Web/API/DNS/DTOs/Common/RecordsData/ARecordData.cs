using DNSLab.Application.Common.Parsers.DNSRecord;

namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record ARecordData(
    string IPv4Address) : IRecordData;
