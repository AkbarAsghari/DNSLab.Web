using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Records;

partial class RecordChangesHistory
{
    [Inject] IRecordRepository _RecordRepository { get; set; }
    [Inject] ISubscriptionRepository _SubscriptionRepository { get; set; }

    [SupplyParameterFromQuery] public Guid? RecordId { get; set; } = null;

    bool? _IsSubscribeThisFeature { get; set; } = null;
    MudDataGrid<RecordChangeDTO> _DataGrid { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsSubscribeThisFeature = await _SubscriptionRepository.CheckSbscriptionFeature(Enums.FeatureEnum.RecordChangesHistory);
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task<GridData<RecordChangeDTO>> ServerReload(GridState<RecordChangeDTO> state)
    {
        IEnumerable<RecordChangeDTO>? data = null;

        if (RecordId == null)
        {
            data = await _RecordRepository.GetTodayRecordChangesHistory();
        }
        else
        {
            _IsSubscribeThisFeature = await _SubscriptionRepository.CheckSbscriptionFeature(Enums.FeatureEnum.RecordChangesHistory);

            if (_IsSubscribeThisFeature is not null && _IsSubscribeThisFeature == true)
            {
                data = await _RecordRepository.GetRecordChangesHistory(RecordId.Value);
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
