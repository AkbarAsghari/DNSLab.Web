using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Helpers;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Subscriptions;

partial class Plans
{
    [Inject] ISubscriptionRepository _SubscriptionRepository { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

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
        if (await _SubscriptionRepository.Subscribe(_SelectedPlan.Id, _SelectedPlanDiscount!.Id))
        {
            _Snackbar.Add($"پلن {_SelectedPlan.Name} به مدت {_SelectedPlanDiscount.Duration.Description} برای شما فعال شد", Severity.Success);
            _NavigationManager.NavigateTo(AllRoutes.Dashboard);
        }
    }
}
