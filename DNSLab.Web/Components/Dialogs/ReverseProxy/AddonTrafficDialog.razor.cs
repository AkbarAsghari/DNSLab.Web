using DNSLab.Web.Components.Dialogs.Invoice;
using DNSLab.Web.Helpers;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.ReverseProxy;

partial class AddonTrafficDialog
{
    [Inject] IDialogService _DialogService { get; set; }
    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    int _Traffic = 10;

    async Task Pay()
    {
        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.ExtraSmall };
        var parameters = new DialogParameters<InvoiceDialog>() {
            { x => x.InvoiceType, Enums.InvoiceTypeEnum.AdditionalTraffic},
            { x => x.Amount , _Traffic * 50_000 },
            { x => x.Traffic, _Traffic },
        };
        var dialog = await _DialogService.ShowAsync<InvoiceDialog>("صورتحساب", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            _MudDialog.Close();
        }
    }
}
