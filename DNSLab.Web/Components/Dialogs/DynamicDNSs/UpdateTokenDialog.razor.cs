using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.DNS.DDNS;
using DNSLab.Web.API.DNS.DDNS.DTOs;
using DNSLab.Web.API.DNS.DTOs.Common;
using DNSLab.Web.Components.Pages.Records;
using DNSLab.Web.DTOs.Repositories.DDNS;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.DynamicDNSs;

partial class UpdateTokenDialog
{
    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    [Inject] IDDNSApiClient DDNSApiClient { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter] public TokenAndRecordsResponse? Token { get; set; } = default;

    List<ZoneAndRecordsResponse>? _ZoneAndRecordsResponses { get; set; }

    string _Name = string.Empty;
    List<Guid> _SelectedRecordIds = new List<Guid>();

    protected override async Task OnInitializedAsync()
    {
        var response = await DDNSApiClient.GetZonesAndARecordsAsync();

        if (response.IsSuccess)
        {
            _ZoneAndRecordsResponses = response.Value!;
        }

        if (Token != default)
        {
            _Name = Token.Name;
            _SelectedRecordIds = Token.Records.Select(x => x.RecordId).ToList();
        }
    }

    void RecordCheckChange(Guid recordId, bool check)
    {
        if (check && !_SelectedRecordIds.Contains(recordId))
        {
            _SelectedRecordIds.Add(recordId);
        }
        else
        {
            _SelectedRecordIds.Remove(recordId);
        }
    }

    async Task Submit()
    {
        Result response;
        if (Token == default)
        {
            response = await DDNSApiClient.CreateTokenAsync(
                 new CreateTokenRequest(_Name, _SelectedRecordIds));
        }
        else
        {
            response = await DDNSApiClient.UpdateTokenAsync(
               new UpdateTokenRequest(Token.TokenId, _Name, _SelectedRecordIds));
        }

        if (response.IsSuccess)
        {
            Snackbar.Add("با موفقیت انجام شد", Severity.Success);
            _MudDialog.Close(DialogResult.Ok(true));
        }
        else
        {
            Snackbar.Add(response.Error!.Message, Severity.Warning);
        }
    }

    private void Cancel() => _MudDialog.Cancel();
}
