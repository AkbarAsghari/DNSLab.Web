using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.Invoice;
using DNSLab.Web.Components.Pages.ReverseProxy;
using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Helpers;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace DNSLab.Web.Components.Pages.Subscriptions;

partial class Plans
{
    [Inject] AuthenticationStateProvider _AuthenticationStateProvider { get; set; }
    [Inject] ISubscriptionRepository _SubscriptionRepository { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }
    [Inject] IDialogService _DialogService { get; set; }


    IEnumerable<PlanSectionDTO>? _PlanSections { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _PlanSections = await _SubscriptionRepository.GetPlans();
    }

    PlanDiscountDTO? _SelectedPlanDiscount { get; set; }

    PlanDTO _SelectedPlan
    {
        get
        {
            if (_SelectedPlanDiscount != null)
            {
                return _PlanSections!.SelectMany(x => x.Plans).First(x => x.Discounts.Any(d => d.Id == _SelectedPlanDiscount.Id));
            }

            return null;
        }
    }

    bool _SubscribeDialogVisible { get; set; } = false;
    void DiscountOnChange(PlanDiscountDTO discount)
    {
        _SelectedPlanDiscount = discount;
        _SubscribeDialogVisible = true;
    }

    async Task Subscriptionn()
    {
        var authState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity != null && authState.User.Identity.IsAuthenticated)
        {
            var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.ExtraSmall };
            var parameters = new DialogParameters<InvoiceDialog>() {
                { x => x.InvoiceType, Enums.InvoiceTypeEnum.Subscription},
                { x => x.Amount , (_SelectedPlanDiscount!.Duration.DurationInMonth * (_SelectedPlan.BasePrice - (_SelectedPlan.BasePrice * _SelectedPlanDiscount.DiscountRate / 100))) },
                { x => x.PlanId, _SelectedPlan.Id },
                { x => x.DiscountId, _SelectedPlanDiscount!.Id },
            };
            var dialog = await _DialogService.ShowAsync<InvoiceDialog>("صورتحساب", parameters, options);
            var result = await dialog.Result;
            if (!result!.Canceled)
            {
                _NavigationManager.NavigateTo(AllRoutes.Dashboard);
            }
        }
        _NavigationManager.NavigateTo(AllRoutes.Login);
    }
}
