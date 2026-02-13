using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs.Ticket;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Tickets;

partial class AllTickets
{
    [Inject] ITicketRepository _TicketRepository { get; set; }

    private async Task<GridData<TicketDTO>> ServerReload(GridState<TicketDTO> state)
    {
        IEnumerable<TicketDTO> ? data = await _TicketRepository.GetAllTickets();


        if (data is null)
        {
            return new GridData<TicketDTO>();
        }

        var totalItems = data.Count();

        var pagedData = data.OrderByDescending(x => x.UpdateDate ?? x.CreateDate).Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

        return new GridData<TicketDTO>
        {
            TotalItems = totalItems,
            Items = pagedData
        };
    }

    async Task OnStatusChanged(TicketDTO ticket, TicketStatusEnum status)
    {
        if (await _TicketRepository.ChangeTicketStatus(ticket.Id, status)) 
        {
            ticket.Status!.Id = (int)status;
        }
    }
}
