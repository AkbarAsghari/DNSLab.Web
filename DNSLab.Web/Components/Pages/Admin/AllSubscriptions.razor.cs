using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Admin;

partial class AllSubscriptions
{
    [Inject] ISubscriptionRepository _SubscriptionsRepository { get; set; }

    MudDataGrid<SubscriptionDTO> _Grid { get; set; }
    private async Task<GridData<SubscriptionDTO>> ServerReload(GridState<SubscriptionDTO> state)
    {
        IEnumerable<SubscriptionDTO>? data = await _SubscriptionsRepository.GetAllSubscribes();

        if (data is null)
        {
            return new GridData<SubscriptionDTO>();
        }

        var totalItems = data.Count();

        var pagedData = data.OrderByDescending(x=>x.CreateDate).Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

        return new GridData<SubscriptionDTO>
        {
            TotalItems = totalItems,
            Items = pagedData
        };
    }
}
