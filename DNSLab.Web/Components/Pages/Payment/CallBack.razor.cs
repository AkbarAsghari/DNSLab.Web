using DNSLab.Web.API.Billing;
using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.Enums;
using DNSLab.Web.Helpers;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Payment;

partial class CallBack
{
    [Inject] IPaymentApiClient PaymentApiClient { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public long? TrackId { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public int? Success { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public int? Status { get; set; }

    bool? IsVerified = null;
    string? Result = null;

    protected override async Task OnInitializedAsync()
    {
        if (TrackId.HasValue)
        {
            var response = await PaymentApiClient.VerifyPaymentAsync(
                 new TrackIdRequest(TrackId.Value));

            IsVerified = response.IsSuccess;
        }

        if (Status.HasValue)
        {
            switch (Status.Value)
            {
                case (int)TransactionStatusEnum.PaidConfirmed:
                    Result = "پرداخت شده - تایید";
                    break;
                case (int)TransactionStatusEnum.PaidUnverified:
                    Result = "پرداخت شده - تاییدنشده";
                    break;
                case (int)TransactionStatusEnum.CanceledByUser:
                    Result = "لغو شده توسط کاربر";
                    break;
                case (int)TransactionStatusEnum.TheCardNumberIsInvalid:
                    Result = "‌شماره کارت نامعتبر می‌باشد";
                    break;
                case (int)TransactionStatusEnum.TheAccountBalanceIsInsufficient:
                    Result = "‌موجودی حساب کافی نمی‌باشد";
                    break;
                case (int)TransactionStatusEnum.TheEnteredPasswordIsWrong:
                    Result = "رمز واردشده اشتباه می‌باشد";
                    break;
                case (int)TransactionStatusEnum.TheIssuerOfTheCardIsInvalid:
                    Result = "‌صادرکننده‌ی کارت نامعتبر می‌باشد";
                    break;
                case (int)TransactionStatusEnum.TheCardIsNotAccessible:
                    Result = "‌کارت قابل دسترسی نمی‌باشد";
                    break;
                case (int)TransactionStatusEnum.TheNumberOfRequestsIsOverTheLimit:
                case (int)TransactionStatusEnum.TheNumberOfOnlinePaymentsPerDayIsMoreThanTheAllowedLimit:
                case (int)TransactionStatusEnum.TheAmountOfDailyInternetPaymentIsMoreThanTheAllowedLimit:
                case (int)TransactionStatusEnum.SwitchError:
                default:
                    Result = "خطای نامعلوم";
                    break;
            }
        }
    }
}
