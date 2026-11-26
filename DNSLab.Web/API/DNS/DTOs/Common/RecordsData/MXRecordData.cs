using DNSLab.Web.API.Common.Parsers.DNSRecord;

namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record MXRecordData(
     UInt16 Preference,
     string Exchange) : IRecordData;
