
using DNSLab.Web.API.Ticket;
using DNSLab.Web.API.Ticket.DTOs;
using DNSLab.Web.API.Ticket.Enums;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DNSLab.Web.Components.Pages.Tickets;

partial class AllTickets
{
    [Inject] ITicketApiClient TicketApiClient { get; set; } = default!;

    private MudDataGrid<TicketResponse> _dataGrid = default!;

    private async Task<GridData<TicketResponse>> ServerReload(GridState<TicketResponse> state)
    {
        var response = await TicketApiClient.GetAllTicketsAsync();

        if (!response.IsSuccess || response.Value is null)
            return new GridData<TicketResponse>();

        var data = response.Value
            .OrderByDescending(x => x.UpdateDate ?? x.CreateDate)
            .ToList();

        return new GridData<TicketResponse>
        {
            TotalItems = data.Count,
            Items = data
                .Skip(state.Page * state.PageSize)
                .Take(state.PageSize)
                .ToArray()
        };
    }

    private async Task OnStatusChanged(Guid ticketId, TicketStatus status)
    {
        var response = await TicketApiClient.ChangeTicketStatusAsync(
            new ChangeTicketStatusRequest(ticketId, status));

        if (response.IsSuccess)
            await _dataGrid.ReloadServerData();
    }
}
