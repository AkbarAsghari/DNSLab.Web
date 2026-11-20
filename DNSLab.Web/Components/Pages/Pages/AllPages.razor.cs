using DNSLab.Web.API.Page;
using DNSLab.Web.API.Page.DTOs;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DNSLab.Web.Components.Pages.Pages;

partial class AllPages
{
    [Inject] IPageApiClient PageApiClient { get; set; }
    [Inject] IDialogService _DialogService { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }

    MudDataGrid<PageInfoResponse> _Grid;
    private async Task<GridData<PageInfoResponse>> ServerReload(GridState<PageInfoResponse> state)
    {
        var response = await PageApiClient.GetAllPagesAsync();

        if (response.IsSuccess)
        {
            var totalItems = response.Value!.Count();

            var pagedData = response.Value!.OrderByDescending(x => x.UpdateDate ?? x.CreateDate).Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

            return new GridData<PageInfoResponse>
            {
                TotalItems = totalItems,
                Items = pagedData
            };
        }
        else
        {
            return new();
        }
    }

    async Task DeletePage(PageInfoResponse page)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {page.Title} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            var response = await PageApiClient.DeletePageAsync(
                new PageIdRequest(page.Id));
            if (response.IsSuccess)
            {
                await _Grid.ReloadServerData();
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }
    }
}
