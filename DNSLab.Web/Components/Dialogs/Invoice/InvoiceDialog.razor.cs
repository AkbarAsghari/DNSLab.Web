using DNSLab.Application.Coupon.DTOs;
using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Coupon;
using DNSLab.Web.API.Subscription;
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
    [Inject] public ISubscriptionApiClient SubscriptionApiClient { get; set; }
    [Inject] public IReverseProxyTrafficRepository _ReverseProxyTrafficRepository { get; set; }
    [Inject] public ICouponApiClient CouponApiClient { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }
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
        var response = await CouponApiClient.ValidateAsync(
            new ValidateCouponRequest(_CouponCode, Amount));

        if (response.IsSuccess)
        {
            _CouponDiscountAmount = response.Value!.DiscountAmount;
        }
        else
        {
            _CouponDiscountAmount = 0;
            Snackbar.Add(response.Error!.Message, Severity.Warning);
        }
    }

    async Task Purchass()
    {
        Result<InvoiceCreationResponse>? response = null;

        switch (InvoiceType)
        {
            case InvoiceTypeEnum.Subscription:
                response = await SubscriptionApiClient.PurchaseSubscribeAsync(
                    new PurchaseSubscriptionRequest(PlanId!.Value, DiscountId.Value, _UseWallet, _CouponCode));
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
                Snackbar.Add("پرداخت با موفقیت انجام شد", Severity.Success);
                _MudDialog.Close();
            }
            else
            {
                _NavigationManager.NavigateTo(response.Value!.PaymentUrl!, true);
            }
        }
    }
}
