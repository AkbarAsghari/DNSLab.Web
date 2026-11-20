using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Ticket.DTOs;

namespace DNSLab.Web.API.Ticket;

public interface ITicketApiClient
{
    Task<Result<Guid>> AddTicketAsync(AddTicketRequest request, CancellationToken cancellationToken = default);
    Task<Result> AddTicketMessageAsync(AddTicketMessageRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteTicketAsync(TicketIdRequest request, CancellationToken cancellationToken = default);
    Task<Result<TicketResponse>> GetTicketMessagesAsync(TicketIdRequest request, CancellationToken cancellationToken = default);
    Task<Result<List<TicketResponse>>> GetAllTicketsAsync(CancellationToken cancellationToken = default);
    Task<Result<List<TicketResponse>>> GetMyTicketsAsync(CancellationToken cancellationToken = default);
    Task<Result> ChangeTicketStatusAsync(ChangeTicketStatusRequest request, CancellationToken cancellationToken = default);
}
