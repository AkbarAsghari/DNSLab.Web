using DNSLab.Application.Common.Parsers.DNSRecord;

namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record CNAMERecordData(
    string CName) : IRecordData;