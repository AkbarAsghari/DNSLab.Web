using DNSLab.Web.API.Ticket;
using DNSLab.Web.API.Ticket.DTOs;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs.Ticket;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Tickets;

partial class MyTickets
{
    [Inject] ITicketApiClient TicketApiClient { get; set; }
    [Inject] IDialogService _DialogService { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    List<TicketResponse>? _Tickets { get; set; }

    bool _IsLoading = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsLoading = true;

            var response = await TicketApiClient.GetMyTicketsAsync();

            if (response.IsSuccess)
            {
                _Tickets = response.Value!;
            }

            _IsLoading = false;
            await InvokeAsync(() => StateHasChanged());
        }
    }

    async Task AddTicket()
    {
        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Medium };

        var dialog = await _DialogService.ShowAsync<AddTicket>("اضافه کردن تیکت جدید", options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await OnAfterRenderAsync(true);
        }
    }

    async Task Delete(TicketResponse ticket)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {ticket.Title} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            var response = await TicketApiClient.DeleteTicketAsync(new TicketIdRequest(ticket.Id));
            if (response.IsSuccess)
            {
                await OnAfterRenderAsync(true);
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }
    }
}
