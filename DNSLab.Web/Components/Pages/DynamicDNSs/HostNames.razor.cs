﻿using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;
using DNSLab.Web.DTOs.Repositories.Zone;
using Microsoft.JSInterop;

namespace DNSLab.Web.Components.Pages.DynamicDNSs
{
    partial class HostNames
    {
        [Inject] IDDNSRepository _DDNSRepository { get; set; }
        [Inject] IRecordRepository _RecordRepository { get; set; }
        [Inject] IDialogService _DialogService { get; set; }
        [Inject] IJSRuntime _JSRuntime { get; set; }

        IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>? _AllRecords { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _AllRecords = await _DDNSRepository.GetDDNSDomainAndRecords();
        }

        async Task Refresh()
        {
            await OnInitializedAsync();
        }

        async Task DisableRecord(BaseRecordDTO record, string zoneName)
        {
            bool _continue = true;

            if (record.Disable == false)
            {
                var parameters = new DialogParameters<BaseDialog>
                {
                    { x => x.ContentText, $"شما در حال غیر فعال سازی {record.Name}.{zoneName} میباشید آیا تایید میکنید؟" },
                    { x => x.ButtonText, "غیر فعال" },
                    { x => x.Color, Color.Warning }
                };

                var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

                var dialog = await _DialogService.ShowAsync<BaseDialog>("غیر فعال سازی", parameters, options);
                var result = await dialog.Result;

                if (result!.Canceled)
                {
                    _continue = false;
                }
            }

            if (_continue && await _RecordRepository.DisableRecord(record.Type, record.Id))
            {
                record.Disable = !record.Disable;
            }
        }

        async Task DeleteRecord(BaseRecordDTO record, string zoneName)
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
                if (await _RecordRepository.DeleteRecord(record.Type, record.Id))
                {
                    await Refresh();
                }
            }
        }

        async Task EditRecord(BaseRecordDTO record , ZoneDTO zone)
        {
            var parameters = new DialogParameters<RecordDialog>() { { "Zone", zone }, { "Record", record.Clone() } };

            var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

            var dialog = await _DialogService.ShowAsync<RecordDialog>("ویرایش رکورد", parameters, options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                await Refresh();
            }
        }

        async Task NewRecord(ZoneDTO zone)
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

        async Task CopyLink(BaseRecordDTO record, ZoneDTO zone)
        {
            await _JSRuntime.InvokeAsync<bool>("clipboardCopy.copyText", $"http://{record.Name}.{zone.Name}".ToLower());
        }
    }
}
