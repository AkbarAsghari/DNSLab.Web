using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Wallet;
using DNSLab.Web.API.Wallet.DTOs;
using DNSLab.Web.DTOs.Repositories.Invoice;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Invoice;

partial class InvoiceDialog
{
    [Inject] public IWalletApiClient WalletApiClient { get; set; }
    [Inject] public ISubscriptionRepository _SubscriptionRepository { get; set; }
    [Inject] public IReverseProxyTrafficRepository _ReverseProxyTrafficRepository { get; set; }
    [Inject] public ICouponRepository _CouponRepository { get; set; }
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

    WalletResponse _MyWallet { get; set; }
    protected override async Task OnInitializedAsync()
    {
        if (InvoiceType == InvoiceTypeEnum.Topup)
        {
            await Purchass();
        }
        else
        {
            var response = await WalletApiClient.GetWalletAsync();

            if (response.IsSuccess)
            {
                _MyWallet = response.Value!;
            }
        }

    }

    string _CouponCode { get; set; } = String.Empty;
    long _CouponDiscountAmount { get; set; } = 0;
    async Task ProcessCouponCode()
    {
        var existCoupon = await _CouponRepository.Validate(_CouponCode, Amount);
        if (existCoupon != null)
        {
            _CouponDiscountAmount = existCoupon.DiscountAmount;
        }
        else
        {
            _CouponDiscountAmount = 0;
        }
    }

    async Task Purchass()
    {
        Result<InvoiceCreationResponse>? response = null;

        switch (InvoiceType)
        {
            case InvoiceTypeEnum.Subscription:
                InvoiceResponseDTO d = await _SubscriptionRepository.PurchaseSubscribe(new PurchaseSubscriptionDTO
                {
                    CouponCode = _CouponCode,
                    PlanId = PlanId!.Value,
                    DiscountId = DiscountId!.Value,
                    UseWallet = _UseWallet,
                });
                break;
            case InvoiceTypeEnum.Topup:
                response = await WalletApiClient.TopupAsync(new PurchaseTopupRequest(TopupAmount!.Value));
                break;
            case InvoiceTypeEnum.AdditionalTraffic:
                InvoiceResponseDTO dd = await _ReverseProxyTrafficRepository.PurchaseAdditionalTraffic(new PurchaseAdditionalTrafficDTO
                {
                    CouponCode = _CouponCode,
                    Traffic = Traffic!.Value,
                    UseWallet = _UseWallet,
                });
                break;
        }

        if (response is not null && response.IsSuccess)
        {
            if (response.Value!.IsComplete)
            {
                _Snackbar.Add("پرداخت با موفقیت انجام شد", Severity.Success);
                _MudDialog.Close();
            }
            else
            {
                _NavigationManager.NavigateTo(response.Value!.PaymentUrl!, true);
            }
        }
    }
}
