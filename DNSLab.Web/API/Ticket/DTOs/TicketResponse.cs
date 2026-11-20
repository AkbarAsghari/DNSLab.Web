using DNSLab.Web.API.Ticket.Enums;

namespace DNSLab.Web.API.Ticket.DTOs;

public sealed record TicketResponse(
    Guid Id,
    Guid UserId,
    string Title,
    TicketStatus Status,
    DateTime CreateDate,
    DateTime? UpdateDate,
    List<TicketMessageResponse> Messages);
