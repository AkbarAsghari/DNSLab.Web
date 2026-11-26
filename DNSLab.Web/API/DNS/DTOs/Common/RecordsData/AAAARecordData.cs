using DNSLab.Web.API.Common.Parsers.DNSRecord;

namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record AAAARecordData(
    string IPv6Address) : IRecordData;