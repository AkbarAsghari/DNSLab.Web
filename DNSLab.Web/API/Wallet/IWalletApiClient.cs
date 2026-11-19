using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.API.Wallet.DTOs;

namespace DNSLab.Web.API.Wallet;

public interface IWalletApiClient
{
    Task<Result<WalletResponse>> GetWalletAsync(CancellationToken cancellationToken = default);

    Task<Result<List<WalletTransactionChartPoint>>> GetLast30DaysTransactionsChartDataAsync(CancellationToken cancellationToken = default);

    Task<Result<PaginationResponse<WalletTransactionResponse>>> GetWalletTransactionsAsync(
        PaginationRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<InvoiceCreationResponse>> TopupAsync(PurchaseTopupRequest request, CancellationToken cancellationToken = default);
}
