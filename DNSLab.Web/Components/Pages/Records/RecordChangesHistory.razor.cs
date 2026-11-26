using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.DNS;
using DNSLab.Web.API.DNS.DTOs;
using DNSLab.Web.API.Subscription;
using DNSLab.Web.API.Subscription.DTOs;
using DNSLab.Web.API.Subscription.Enums;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Records;

partial class RecordChangesHistory
{
    [Inject] IDNSApiClient DNSApiClient { get; set; }
    [Inject] ISubscriptionApiClient SubscriptionApiClient { get; set; }

    [SupplyParameterFromQuery] public Guid RecordId { get; set; }

    bool? _IsSubscribeThisFeature { get; set; } = null;
    MudDataGrid<RecordChangeResponse> _DataGrid { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var response = await SubscriptionApiClient.CheckSubscriptionFeatureAsync(
                new FeatureRequest(FeatureEnum.RecordChangesHistory));

            _IsSubscribeThisFeature = response.IsSuccess;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task<GridData<RecordChangeResponse>> ServerReload(GridState<RecordChangeResponse> state)
    {
        var response = await DNSApiClient.GetRecordChangeHistoryAsync(
            new RecordChangeRequest(RecordId, new PaginationRequest(state.Page, state.PageSize)));

        if (!response.IsSuccess)
        {
            return new GridData<RecordChangeResponse>();
        }

        return new GridData<RecordChangeResponse>
        {
            TotalItems = response.Value!.TotalCount,
            Items = response.Value!.Items
        };
    }
}
