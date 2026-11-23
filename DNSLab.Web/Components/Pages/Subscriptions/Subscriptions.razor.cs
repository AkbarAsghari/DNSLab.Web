using DNSLab.Web.API.Subscription;
using DNSLab.Web.API.Subscription.DTOs;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Subscriptions;

partial class Subscriptions
{
    [Inject] ISubscriptionApiClient SubscriptionApiClient { get; set; }

    MudDataGrid<SubscriptionResponse> _Grid { get; set; }
    private async Task<GridData<SubscriptionResponse>> ServerReload(GridState<SubscriptionResponse> state)
    {
        var response = await SubscriptionApiClient.GetSubscribesAsync();
        if (!response.IsSuccess)
        {
            return new GridData<SubscriptionResponse>();
        }

        IEnumerable<SubscriptionResponse>? data = response.Value!;

        var totalItems = data.Count();

        var pagedData = data.OrderByDescending(x => x.CreateDate).Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

        return new GridData<SubscriptionResponse>
        {
            TotalItems = totalItems,
            Items = pagedData
        };
    }
}
