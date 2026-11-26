
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Subscription;
using DNSLab.Web.API.Zone;
using DNSLab.Web.API.Zone.DTOs;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.Zone;
using DNSLab.Web.DTOs.Repositories.Zone;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Zone
{
    partial class AllZones
    {
        [Inject] IZoneApiClient ZoneApiClient { get; set; }
        [Inject] IDialogService _DialogService { get; set; }
        [Inject] ISubscriptionApiClient SubscriptionApiClient { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        MudDataGrid<ZoneResponse> _Grid { get; set; }

        private async Task<GridData<ZoneResponse>> ServerReload(GridState<ZoneResponse> state)
        {
            var response = await ZoneApiClient.GetZonesAsync();

            if (!response.IsSuccess)
            {
                return new GridData<ZoneResponse>();
            }

            var data = response.Value!;

            var totalItems = data.Count();

            return new GridData<ZoneResponse>
            {
                TotalItems = totalItems,
                Items = data
            };
        }

        async Task NewZone()
        {
            var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

            var dialog = await _DialogService.ShowAsync<AddZoneDialog>("اضافه کردن دامنه جدید", options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                await _Grid.ReloadServerData();
            }
        }

        async Task DeleteZone(ZoneResponse zone)
        {
            var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {zone.Name} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                var response = await ZoneApiClient.DeleteZoneAsync(
                    new ZoneIdRequest(zone.Id));

                if (response.IsSuccess)
                {
                    await _Grid.ReloadServerData();
                    Snackbar.Add("دامنه با موفقیت حذف شد", Severity.Success);
                }
                else
                {
                    Snackbar.Add(response.Error!.Message, Severity.Warning);
                }
            }
        }

        async Task DisableZone(ZoneResponse zone)
        {
            bool _continue = true;

            if (zone.Disable == false)
            {
                var parameters = new DialogParameters<BaseDialog>
                {
                    { x => x.ContentText, $"شما در حال غیر فعال سازی {zone.Name} میباشید آیا تایید میکنید؟" },
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

            Result response;

            if (zone.Disable)
            {
                response = await ZoneApiClient.EnableZoneAsync(
                    new ZoneIdRequest(zone.Id));
            }
            else
            {
                response = await ZoneApiClient.DisableZoneAsync(
                    new ZoneIdRequest(zone.Id));
            }
            

            if (_continue && response.IsSuccess)
            {
                await _Grid.ReloadServerData();
                Snackbar.Add("تغییرات انجام شد", Severity.Success);
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }
    }
}
