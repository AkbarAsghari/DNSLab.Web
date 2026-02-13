using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.DTOs.Repositories.Ticket;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Pages;

partial class AllPages
{
    [Inject] IPageRepository _PageRepository { get; set; }
    [Inject] IDialogService _DialogService { get; set; }

    MudDataGrid<PageInfoDTO> _Grid;
    private async Task<GridData<PageInfoDTO>> ServerReload(GridState<PageInfoDTO> state)
    {
        IEnumerable<PageInfoDTO>? data = await _PageRepository.GetAllPages();


        if (data is null)
        {
            return new GridData<PageInfoDTO>();
        }

        var totalItems = data.Count();

        var pagedData = data.OrderByDescending(x => x.UpdateDate ?? x.CreateDate).Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

        return new GridData<PageInfoDTO>
        {
            TotalItems = totalItems,
            Items = pagedData
        };
    }

    async Task DeletePage(PageInfoDTO page)
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
            if (await _PageRepository.DeletePage(page.Id))
            {
                await _Grid.ReloadServerData();
            }
        }
    }
}
