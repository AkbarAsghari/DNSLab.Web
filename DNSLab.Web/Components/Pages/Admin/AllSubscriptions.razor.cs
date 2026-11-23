using DNSLab.Web.API.Subscription;
using DNSLab.Web.API.Subscription.DTOs;
using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Admin;

partial class AllSubscriptions
{
    [Inject] ISubscriptionApiClient SubscriptionApiClient { get; set; }

    MudDataGrid<AllSubscriptionResponse> _Grid { get; set; }
    private async Task<GridData<AllSubscriptionResponse>> ServerReload(GridState<AllSubscriptionResponse> state)
    {
        var response = await SubscriptionApiClient.GetAllSubscribesAsync();

        if (!response.IsSuccess)
        {
            return new GridData<AllSubscriptionResponse>();
        }

        IEnumerable<AllSubscriptionResponse> data = response.Value!;

        var totalItems = data.Count();

        var pagedData = data.OrderByDescending(x => x.CreateDate).Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

        return new GridData<AllSubscriptionResponse>
        {
            TotalItems = totalItems,
            Items = pagedData
        };
    }
}
