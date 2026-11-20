using DNSLab.Web.API.Ticket;
using DNSLab.Web.API.Ticket.DTOs;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Ticket;

partial class AddTicket
{
    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    [Inject] ITicketApiClient TicketApiClient { get; set; }

    string _Title = string.Empty;
    string _Message = string.Empty;

    async Task Submit()
    {
        var response = await TicketApiClient.AddTicketAsync(
            new AddTicketRequest(_Title, _Message));

        if (response.IsSuccess)
        {
            _MudDialog.Close(DialogResult.Ok(true));
        }
    }

    private void Cancel() => _MudDialog.Cancel();
}
