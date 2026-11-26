using DNSLab.Web.API.DNS.DTOs.Common.RecordsData;
using DNSLab.Web.API.DNS.Enums;
using DNSLab.Web.API.DNS.Interfaces;
using System.Text.Json;

namespace DNSLab.Web.Extensions;

public static class RecordDataExtensions
{
    public static IRecordData Parse(this JsonElement data, RecordType type)
    {
        return type switch
        {
            RecordType.A =>
                data.Deserialize<ARecordData>()
                ?? throw new InvalidOperationException(),

            RecordType.AAAA =>
                data.Deserialize<AAAARecordData>()
                ?? throw new InvalidOperationException(),

            RecordType.CNAME =>
                data.Deserialize<CNAMERecordData>()
                ?? throw new InvalidOperationException(),

            RecordType.MX =>
                data.Deserialize<MXRecordData>()
                ?? throw new InvalidOperationException(),

            RecordType.TXT =>
                data.Deserialize<TXTRecordData>()
                ?? throw new InvalidOperationException(),

            _ => throw new NotSupportedException(
                $"Record type '{type}' is not supported.")
        };
    }
}