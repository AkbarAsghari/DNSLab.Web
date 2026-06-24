using DNSLab.Web.Components.Dialogs.Invoice;
using DNSLab.Web.Components.Pages.ReverseProxy;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Wallet;

partial class IncreaseBalanceDialog
{
    [Inject] IPaymentRepository _PaymentRepository { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }
    [Inject] IDialogService _DialogService { get; set; }

    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    int _Amount;
    string _DisplayAmount = String.Empty;
    string _ErrorMessage = String.Empty;
    private void OnAmountTextChanged(string newValue)
    {
        var raw = newValue.Replace(",", "");
        if (int.TryParse(raw, out var parsed))
        {
            if (parsed == 0)
            {
                parsed = 500_000; //Rials
            }
            _Amount = parsed;
            _DisplayAmount = parsed.ToString("N0");
            _ErrorMessage = String.Empty;
        }
        else
        {
            _ErrorMessage = "مقدار وارد شده صحیح نیست";
        }
    }
    async Task Pay()
    {
        if (_Amount < 50_000 || _Amount > 500_000_000)
        {
            _ErrorMessage = "مقدار وارد شده باید بین 50،000 تا 500،000،000 ریال باشد";
            return;
        }


        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.ExtraSmall };
        var parameters = new DialogParameters<InvoiceDialog>() {
            { x => x.InvoiceType, Enums.InvoiceTypeEnum.Topup},
            { x => x.Amount , _Amount },
            { x => x.TopupAmount, _Amount },
        };
        var dialog = await _DialogService.ShowAsync<InvoiceDialog>("صورتحساب", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            _MudDialog.Close();
        }
    }
}
