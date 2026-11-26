using DNSLab.Web.API.DNS;
using DNSLab.Web.API.DNS.DTOs.Common;
using DNSLab.Web.API.Zone;
using DNSLab.Web.API.Zone.DTOs;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.DTOs.Repositories.Record;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Records;

partial class AllRecords
{
    [Inject] IZoneApiClient ZoneApiClient { get; set; }
    [Inject] IDNSApiClient DNSApiClient { get; set; }
    [Inject] IDialogService _DialogService { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter] public Guid ZoneId { get; set; }

    ZoneAndRecordsResponse? _ZoneAndRecordsResponse { get; set; }
    IEnumerable<string>? _RequiredToUpdateNameServers { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var response = await DNSApiClient.GetZoneRecordsAsync(
            new ZoneIdRequest(ZoneId));

        if (response.IsSuccess)
        {
            _ZoneAndRecordsResponse = response.Value!;
            await InvokeAsync(StateHasChanged);
        }
        else
        {
            Snackbar.Add(response.Error!.Message, Severity.Warning);
        }
    }

    //async Task DisableRecord(RecordResponse record)
    //{
    //    bool _continue = true;

    //    if (record.Disable == false)
    //    {
    //        var parameters = new DialogParameters<BaseDialog>
    //            {
    //                { x => x.ContentText, $"شما در حال غیر فعال سازی {record.Name}.{_ZoneAndRecordsResponse!.Zone.Name} میباشید آیا تایید میکنید؟" },
    //                { x => x.ButtonText, "غیر فعال" },
    //                { x => x.Color, Color.Warning }
    //            };

    //        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

    //        var dialog = await _DialogService.ShowAsync<BaseDialog>("غیر فعال سازی", parameters, options);
    //        var result = await dialog.Result;

    //        if (result!.Canceled)
    //        {
    //            _continue = false;
    //        }
    //    }

    //    if (_continue && await _RecordRepository.DisableRecord(record.Type, record.Id))
    //    {
    //        record.Disable = !record.Disable;
    //    }
    //}

    async Task DeleteRecord(RecordResponse record)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {record.Name}.{_ZoneAndRecordsResponse!.Zone.Name} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            var response = await DNSApiClient.DeleteRecordAsync(
                new DeleteRecordRequest(record.RecordId, record.Type));

            if (response.IsSuccess)
            {
                Snackbar.Add("رکورد با موفقیت حذف شد", Severity.Success);
                await OnInitializedAsync();
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Success);
            }
        }
    }

    async Task NewRecord()
    {
        var parameters = new DialogParameters<RecordDialog>() { { "Zone", _ZoneAndRecordsResponse!.Zone } };

        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<RecordDialog>("اضافه کردن رکورد جدید", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await OnInitializedAsync();
        }
    }

    async Task EditRecord(RecordResponse record)
    {
        var parameters = new DialogParameters<RecordDialog>() { { "Zone", _ZoneAndRecordsResponse!.Zone }, { "Record", record } };

        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<RecordDialog>("ویرایش رکورد", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await OnInitializedAsync();
        }
    }
}
