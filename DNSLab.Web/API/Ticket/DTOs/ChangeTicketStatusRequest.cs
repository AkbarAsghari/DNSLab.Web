using DNSLab.Web.API.Ticket.Enums;

namespace DNSLab.Web.API.Ticket.DTOs;

public sealed record ChangeTicketStatusRequest(
    Guid TicketId,
    TicketStatus Status);
