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
    [Inject] ISubscriptionRepository _SubscriptionsRepository { get; set; }

    MudDataGrid<SubscriptionDTO> _Grid { get; set; }
    private async Task<GridData<SubscriptionDTO>> ServerReload(GridState<SubscriptionDTO> state)
    {
        IEnumerable<SubscriptionDTO>? data = await _SubscriptionsRepository.GetSubscribes();

        if (data is null)
        {
            return new GridData<SubscriptionDTO>();
        }

        var totalItems = data.Count();

        var pagedData = data.OrderByDescending(x => x.CreateDate).Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

        return new GridData<SubscriptionDTO>
        {
            TotalItems = totalItems,
            Items = pagedData
        };
    }
}
