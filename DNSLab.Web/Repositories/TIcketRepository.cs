﻿using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class TicketRepository(IHttpServiceProvider _HttpServiceProvider) : ITicketRepository
    {
        const string APIController = "Ticket";
        public async Task<bool> AddTicket(TicketDTO model)
        {
            return await _HttpServiceProvider.Post<TicketDTO, bool>($"{APIController}/AddTicket", model);
        }

        public async Task<bool> AddTicketMessage(TicketMessageDTO model)
        {
            return await _HttpServiceProvider.Post<TicketMessageDTO, bool>($"{APIController}/AddTicketMessage", model);
        }

        public async Task<bool> ChangeTicketStatus(Guid id, TicketStatusEnum status)
        {
            return await _HttpServiceProvider.Put<bool>($"{APIController}/ChangeTicketStatus?id={id}&status={(int)status}");
        }

        public async Task<bool> DeleteTicket(Guid id)
        {
            return await _HttpServiceProvider.Delete<bool>($"{APIController}/DeleteTicket?id={id}");
        }

        public async Task<List<TicketDTO>?> GetAllTickets()
        {
            return await _HttpServiceProvider.Get<List<TicketDTO>?>($"{APIController}/GetAllTickets");
        }

        public async Task<List<TicketDTO>?> GetMyTickets()
        {
            return await _HttpServiceProvider.Get<List<TicketDTO>?>($"{APIController}/GetMyTickets");
        }

        public async Task<TicketDTO?> GetTicketMessages(Guid ticketId)
        {
            return await _HttpServiceProvider.Get<TicketDTO?>($"{APIController}/GetTicketMessages?ticketId={ticketId}");
        }
    }
}
