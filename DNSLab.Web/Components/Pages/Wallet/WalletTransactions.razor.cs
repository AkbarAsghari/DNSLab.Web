using DNSLab.Web.API.Common.Pagination;
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

    PaginationResponse<WalletTransactionResponse>? _AllTransactions;
    protected override async Task OnInitializedAsync()
    {
    }

    bool _Loading = false;
    async Task PageChanged(int page)
    {
        _Loading = true;
        var responnse = await WalletApiClient.GetWalletTransactionsAsync(new PaginationRequest(page, PageSize: 10));
        if (responnse.IsSuccess)
        {
            _AllTransactions = responnse.Value;
        }
        _Loading = false;
    }
}
