namespace DNSLab.Web.API.Ticket.DTOs;

public sealed record AddTicketMessageRequest(
    Guid TicketId,
    string Message);
