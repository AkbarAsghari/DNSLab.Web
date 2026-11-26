using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.DTOs.Repositories.Record;
using Microsoft.AspNetCore.Components;
using DNSLab.Web.DTOs.Repositories.Zone;
using Microsoft.JSInterop;
using DNSLab.Web.API.DNS.DDNS;
using DNSLab.Web.API.DNS;
using DNSLab.Web.API.DNS.DTOs.Common;
using DNSLab.Web.API.Zone.DTOs;

namespace DNSLab.Web.Components.Pages.DynamicDNSs
{
    partial class HostNames
    {
        [Inject] IDDNSApiClient DDNSApiClient { get; set; }
        [Inject] IDNSApiClient DNSApiClient { get; set; }
        [Inject] IDialogService _DialogService { get; set; }
        [Inject] IJSRuntime _JSRuntime { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        List<ZoneAndRecordsResponse>? _ZonesAndRecords { get; set; } = default;

        protected override async Task OnInitializedAsync()
        {
            var response = await DDNSApiClient.GetZonesAndRecordsAsync();
            if (response.IsSuccess)
            {
                _ZonesAndRecords = response.Value;
            }
        }

        async Task Refresh()
        {
            await OnInitializedAsync();
        }

        //async Task DisableRecord(BaseRecordDTO record, string zoneName)
        //{
        //    bool _continue = true;

        //    if (record.Disable == false)
        //    {
        //        var parameters = new DialogParameters<BaseDialog>
        //        {
        //            { x => x.ContentText, $"شما در حال غیر فعال سازی {record.Name}.{zoneName} میباشید آیا تایید میکنید؟" },
        //            { x => x.ButtonText, "غیر فعال" },
        //            { x => x.Color, Color.Warning }
        //        };

        //        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        //        var dialog = await _DialogService.ShowAsync<BaseDialog>("غیر فعال سازی", parameters, options);
        //        var result = await dialog.Result;

        //        if (result!.Canceled)
        //        {
        //            _continue = false;
        //        }
        //    }

        //    if (_continue && await DDNSApiClient.(record.Type, record.Id))
        //    {
        //        record.Disable = !record.Disable;
        //    }
        //}

        async Task DeleteRecord(RecordResponse record, string zoneName)
        {
            var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {record.Name}.{zoneName} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                var response = await DDNSApiClient.DeleteRecordAsync(
                    new DeleteRecordRequest(record.RecordId, record.Type));

                if (response.IsSuccess)
                {
                    Snackbar.Add("رکورد با موفقیت حذف شد", Severity.Success);
                    await Refresh();
                }
                else
                {
                    Snackbar.Add(response.Error!.Message, Severity.Warning);
                }
            }
        }

        async Task EditRecord(RecordResponse record, ZoneResponse zone)
        {
            var parameters = new DialogParameters<RecordDialog>() { { "Zone", zone }, { "Record", record } };

            var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

            var dialog = await _DialogService.ShowAsync<RecordDialog>("ویرایش رکورد", parameters, options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                await Refresh();
            }
        }

        async Task NewRecord(ZoneResponse zone)
        {
            var parameters = new DialogParameters<RecordDialog>() { { "Zone", zone }, { "IsDDNS", true } };

            var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

            var dialog = await _DialogService.ShowAsync<RecordDialog>("اضافه کردن هاست نِیم جدید", parameters, options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                await Refresh();
            }
        }

        async Task CopyLink(RecordResponse record, ZoneResponse zone)
        {
            await _JSRuntime.InvokeAsync<bool>("clipboardCopy.copyText", $"http://{record.Name}.{zone.Name}".ToLower());
        }
    }
}
