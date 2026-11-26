using DNSLab.Application.Common.Parsers.DNSRecord;

namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record TXTRecordData(
    string CName,
    string TextData) : IRecordData;
