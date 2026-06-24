using DNSLab.Web.DTOs.Repositories.Invoice;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Invoice;

partial class InvoiceDialog
{
    [Inject] public IWalletRepository _WalletRepository { get; set; }
    [Inject] public ISubscriptionRepository _SubscriptionRepository { get; set; }
    [Inject] public IReverseProxyTrafficRepository _ReverseProxyTrafficRepository { get; set; }
    [Inject] public ISnackbar _Snackbar { get; set; }
    [Inject] public NavigationManager _NavigationManager { get; set; }

    [Parameter] public InvoiceTypeEnum InvoiceType { get; set; }

    [Parameter] public int Amount { get; set; }

    [Parameter] public int? Traffic { get; set; }

    [Parameter] public int? PlanId { get; set; }
    [Parameter] public int? DiscountId { get; set; }

    [Parameter] public int? TopupAmount { get; set; }

    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    bool _UseWallet { get; set; } = false;

    WalletDTO _MyWallet { get; set; }
    protected override async Task OnInitializedAsync()
    {
        if (InvoiceType == InvoiceTypeEnum.Topup)
        {
            await Purchass();
        }
        else
        {
            var wallet = await _WalletRepository.GetWallet();

            if (wallet != null)
            {
                _MyWallet = wallet;
            }
        }

    }


    async Task Purchass()
    {
        InvoiceResponseDTO? purchaseResponse = null;

        switch (InvoiceType)
        {
            case InvoiceTypeEnum.Subscription:
                purchaseResponse = await _SubscriptionRepository.PurchaseSubscribe(new PurchaseSubscriptionDTO
                {
                    PlanId = PlanId!.Value,
                    DiscountId = DiscountId!.Value,
                    UseWallet = _UseWallet,
                });
                break;
            case InvoiceTypeEnum.Topup:
                purchaseResponse = await _WalletRepository.Topup(new PurchaseTopupDTO
                {
                    Amount = TopupAmount!.Value,
                });
                break;
            case InvoiceTypeEnum.AdditionalTraffic:
                purchaseResponse = await _ReverseProxyTrafficRepository.PurchaseAdditionalTraffic(new PurchaseAdditionalTrafficDTO
                {
                    Traffic = Traffic!.Value,
                    UseWallet = _UseWallet,
                });
                break;
        }

        if (purchaseResponse != null)
        {
            if (purchaseResponse.IsComplete)
            {
                _Snackbar.Add("پرداخت با موفقیت انجام شد", Severity.Success);
                _MudDialog.Close();
            }
            else
            {
                _NavigationManager.NavigateTo(purchaseResponse.PaymentUrl!, true);
            }
        }
    }
}
