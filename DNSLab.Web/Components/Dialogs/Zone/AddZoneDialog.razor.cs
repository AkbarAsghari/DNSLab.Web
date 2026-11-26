using DNSLab.Web.API.Zone;
using DNSLab.Web.API.Zone.DTOs;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Zone;

partial class AddZoneDialog
{
    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    [Inject] IZoneApiClient ZoneApiClient { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    string _Name = string.Empty;

    async Task Submit()
    {
        var response = await ZoneApiClient.CreateZoneAsync(
            new CreateZoneRequest(_Name));

        if (response.IsSuccess)
        {
            _MudDialog.Close(DialogResult.Ok(true));
        }
        else
        {
            Snackbar.Add(response.Error!.Message, Severity.Warning);
        }
    }

    private void Cancel() => _MudDialog.Cancel();
}
