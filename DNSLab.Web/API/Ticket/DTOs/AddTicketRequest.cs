namespace DNSLab.Web.API.Ticket.DTOs;

public sealed record AddTicketRequest(
    string Title,
    string Message);
