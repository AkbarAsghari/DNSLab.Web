using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.Subscription;
using DNSLab.Web.API.Subscription.DTOs;
using DNSLab.Web.API.Wallet;
using DNSLab.Web.API.Wallet.DTOs;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Wallet;

partial class WalletTransactions
{
    [Inject] IWalletApiClient WalletApiClient { get; set; }

    private async Task<GridData<WalletTransactionResponse>> ServerReload(GridState<WalletTransactionResponse> state)
    {
        var responnse = await WalletApiClient.GetWalletTransactionsAsync(new PaginationRequest(state.Page, PageSize: state.PageSize));
        if (!responnse.IsSuccess)
        {
            return new GridData<WalletTransactionResponse>();
        }

        return new GridData<WalletTransactionResponse>
        {
            TotalItems = responnse.Value!.TotalCount,
            Items = responnse.Value!.Items
        };
    }
}
