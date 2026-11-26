using DNSLab.Web.API.DNS;
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

    [SupplyParameterFromQuery] public Guid? RecordId { get; set; } = null;

    bool? _IsSubscribeThisFeature { get; set; } = null;
    MudDataGrid<RecordChangeDTO> _DataGrid { get; set; }

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

    private async Task<GridData<RecordChangeDTO>> ServerReload(GridState<RecordChangeDTO> state)
    {
        IEnumerable<RecordChangeDTO>? data = null;

        if (RecordId == null)
        {
            //data = await _RecordRepository.GetTodayRecordChangesHistory();
        }
        else
        {
            var response = await SubscriptionApiClient.CheckSubscriptionFeatureAsync(
                new FeatureRequest(FeatureEnum.RecordChangesHistory));

            _IsSubscribeThisFeature = response.IsSuccess;

            if (_IsSubscribeThisFeature is not null && _IsSubscribeThisFeature == true)
            {
                //data = await _RecordRepository.GetRecordChangesHistory(RecordId.Value);
            }
        }

        if (data is null)
        {
            return new GridData<RecordChangeDTO>();
        }

        data = data.OrderByDescending(x => x.TimeStamp);

        var totalItems = data.Count();

        var pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

        return new GridData<RecordChangeDTO>
        {
            TotalItems = totalItems,
            Items = pagedData
        };
    }


}
