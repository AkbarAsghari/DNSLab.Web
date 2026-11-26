namespace DNSLab.Web.API.DNS.DTOs.Common.RecordsData;

public record TXTRecordData(
    string CName,
    string TextData);
