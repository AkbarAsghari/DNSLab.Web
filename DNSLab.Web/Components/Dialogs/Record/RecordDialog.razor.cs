using ApexCharts;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.DNS;
using DNSLab.Web.API.DNS.DDNS;
using DNSLab.Web.API.DNS.DTOs.Common;
using DNSLab.Web.API.DNS.DTOs.Common.RecordsData;
using DNSLab.Web.API.DNS.Enums;
using DNSLab.Web.API.Subscription;
using DNSLab.Web.API.Subscription.DTOs;
using DNSLab.Web.API.Subscription.Enums;
using DNSLab.Web.API.Zone.DTOs;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace DNSLab.Web.Components.Dialogs.Record;

partial class RecordDialog
{
    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    [Inject] IDDNSApiClient DDNSApiClient { get; set; }
    [Inject] IDNSApiClient DNSApiClient { get; set; }
    [Inject] IHttpContextAccessor _HttpContextAccessor { get; set; }
    [Inject] ISubscriptionApiClient SubscriptionApiClient { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter] public ZoneResponse Zone { get; set; }
    [Parameter] public RecordResponse? Record { get; set; } = default;
    [Parameter] public bool IsDDNS { get; set; } = false;

    //SameData
    string _Name { get; set; } = string.Empty;
    string _Comment { get; set; } = string.Empty;
    uint _TTL { get; set; }
    RecordType _Type { get; set; } = RecordType.A;

    //A
    string _IPv4Address { get; set; }

    //AAAA
    string _IPv6Address { get; set; }

    //TXT
    string _TextData { get; set; }

    //CName
    string _CName { get; set; }

    //MX
    string _Exchange { get; set; }
    ushort _Preference { get; set; }

    bool _IsAbleToChangeTTL = false;
    protected override async Task OnInitializedAsync()
    {
        var response = await SubscriptionApiClient.CheckSubscriptionFeatureAsync(
            new FeatureRequest(FeatureEnum.AbleToChangeTTL));

        _IsAbleToChangeTTL = response.IsSuccess;
    }

    protected override void OnInitialized()
    {
        if (Record is null)
        {
            Record = new(
                RecordId: Guid.Empty,
                Name: string.Empty,
                TTL: (uint)(IsDDNS ? 300 : 3600),
                Comment: string.Empty,
                Disable: false,
                LastModified: DateTime.UtcNow,
                Data: default,
                Type: RecordType.A);
        }
        else
        {
            switch (Record.Type)
            {
                case RecordType.A:
                    var A = (ARecordData)Record.Data.Parse(Record.Type);
                    _IPv4Address = A.IPv4Address;
                    break;
                case RecordType.AAAA:
                    var AAAA = (AAAARecordData)Record.Data.Parse(Record.Type);
                    _IPv6Address = AAAA.IPv6Address;
                    break;
                case RecordType.TXT:
                    var TXT = (TXTRecordData)Record.Data.Parse(Record.Type);
                    _TextData = TXT.TextData;
                    break;
                case RecordType.CNAME:
                    var CName = (CNAMERecordData)Record.Data.Parse(Record.Type);
                    _CName = CName.CName;
                    break;
                case RecordType.MX:
                    var MX = (MXRecordData)Record.Data.Parse(Record.Type);
                    _Preference = MX.Preference;
                    _Exchange = MX.Exchange;
                    break;
                default:
                    break;
            }
        }

        _Type = Record.Type;
        _Name = Record.Name;
        _Comment = Record.Comment;
        _TTL = Record.TTL;
    }

    async Task Submit()
    {
        JsonElement data = default;
        switch (_Type)
        {
            case RecordType.A:
                //Set User IP if ipv4 address was empty
                var ip = _IPv4Address ??= _HttpContextAccessor.HttpContext?.Connection?.RemoteIpAddress!.ToString()!;
                data = JsonSerializer.SerializeToElement(new ARecordData(ip));
                break;
            case RecordType.AAAA:
                data = JsonSerializer.SerializeToElement(new AAAARecordData(_IPv6Address));
                break;
            case RecordType.TXT:
                data = JsonSerializer.SerializeToElement(new TXTRecordData(_TextData));
                break;
            case RecordType.CNAME:
                data = JsonSerializer.SerializeToElement(new CNAMERecordData(_CName));
                break;
            case RecordType.MX:
                data = JsonSerializer.SerializeToElement(new MXRecordData(_Preference, _Exchange));
                break;
        }

        Result response;

        switch (Record == default || Record.RecordId == Guid.Empty, IsDDNS)
        {
            case (true, true):
                response = await DDNSApiClient.AddRecordAsync(
                    new AddRecordRequest(Zone.Id, _Name, _TTL, _Comment, data, _Type));
                break;
            case (true, false):
                response = await DNSApiClient.AddRecordAsync(
                    new AddRecordRequest(Zone.Id, _Name, _TTL, _Comment, data, _Type));
                break;
            case (false, true):
                response = await DDNSApiClient.UpdateRecordAsync(
                    new UpdateRecordRequest(Record!.RecordId, _Name, _TTL, _Comment, data, _Type));
                break;
            case (false, false):
                response = await DNSApiClient.UpdateRecordAsync(
                    new UpdateRecordRequest(Record!.RecordId, _Name, _TTL, _Comment, data, _Type));
                break;
        }


        if (response.IsSuccess)
        {
            Snackbar.Add($"رکورد با موفقیت {(Record == default ? "اضافه" : "ویرایش")} شد", Severity.Success);
            _MudDialog.Close(DialogResult.Ok(true));
        }
        else
        {
            Snackbar.Add(response.Error!.Message, Severity.Warning);
        }

    }

    private void Cancel() => _MudDialog.Cancel();
}
