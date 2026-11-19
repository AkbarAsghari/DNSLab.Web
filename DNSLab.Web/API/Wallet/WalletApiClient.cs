using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Authentication.DTOs;
using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Wallet.DTOs;

namespace DNSLab.Web.API.Wallet;

public class WalletApiClient(IApiClient apiClient) : IWalletApiClient
{
    const string _Prefix = "api/wallet/";

    public Task<Result<List<WalletTransactionChartPoint>>> GetLast30DaysTransactionsChartDataAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<List<WalletTransactionChartPoint>>(
            $"{_Prefix}GetLast30DaysTransactionsChartData",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<WalletResponse>> GetWalletAsync(CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<WalletResponse>(
            $"{_Prefix}GetWallet",
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<PaginationResponse<WalletTransactionResponse>>> GetWalletTransactionsAsync(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PaginationRequest, PaginationResponse<WalletTransactionResponse>>(
            $"{_Prefix}GetWalletTransactions",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }

    public Task<Result<InvoiceCreationResponse>> TopupAsync(PurchaseTopupRequest request, CancellationToken cancellationToken = default)
    {
        return apiClient.GetAsync<PurchaseTopupRequest, InvoiceCreationResponse>(
            $"{_Prefix}Topup",
            request,
            new ApiRequestOptions { RequiresAuthentication = true },
            cancellationToken: cancellationToken);
    }
}
