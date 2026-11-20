namespace DNSLab.Web.API.Ticket.DTOs;

public sealed record TicketMessageResponse(
    Guid Id,
    Guid TicketId,
    Guid UserId,
    string Message,
    DateTime CreateDate);
