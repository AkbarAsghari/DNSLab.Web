using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Ticket.DTOs;

namespace DNSLab.Web.API.Ticket;

public class TicketApiClient(IApiClient apiClient) : ITicketApiClient
{
    const string _Prefix = "api/ticket/";

    public Task<Result<Guid>> AddTicketAsync(AddTicketRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync<AddTicketRequest, Guid>(
            $"{_Prefix}AddTicket",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> AddTicketMessageAsync(AddTicketMessageRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PostAsync(
            $"{_Prefix}AddTicketMessage",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> ChangeTicketStatusAsync(ChangeTicketStatusRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.PutAsync(
            $"{_Prefix}ChangeTicketStatus",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result> DeleteTicketAsync(TicketIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.DeleteAsync(
            $"{_Prefix}DeleteTicket",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<TicketResponse>>> GetAllTicketsAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<TicketResponse>>(
            $"{_Prefix}GetAllTickets",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<List<TicketResponse>>> GetMyTicketsAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<TicketResponse>>(
            $"{_Prefix}GetMyTickets",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<TicketResponse>> GetTicketMessagesAsync(TicketIdRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<TicketIdRequest, TicketResponse>(
            $"{_Prefix}GetTicketMessages",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }
}
