using DNSLab.Web.API.Ticket;
using DNSLab.Web.API.Ticket.DTOs;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Tickets;

partial class TicketMessages
{
    [Inject] ITicketApiClient TicketApiClient { get; set; }

    [Parameter] public Guid TicketId { get; set; }

    TicketResponse? _Ticket { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var response = await TicketApiClient.GetTicketMessagesAsync(new TicketIdRequest(TicketId));

            if (response.IsSuccess)
            {
                _Ticket = response.Value!;
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    string _Message { get; set; } = string.Empty;

    async Task SendMessage()
    {
        var response = await TicketApiClient.AddTicketMessageAsync(new AddTicketMessageRequest(TicketId, _Message));
        if (response.IsSuccess)
        {
            _Message = String.Empty;
            await OnAfterRenderAsync(true);
        }
    }
}
