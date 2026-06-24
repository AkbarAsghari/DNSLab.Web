using DNSLab.Web.DTOs.Repositories.Invoice;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Wallet;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<WalletDTO?> GetWallet();
        Task<PagedResult<WalletTransactionDTO>?> GetWalletTransactions(int page = 1, int pageSize = 10);
        Task<IEnumerable<Tuple<DateTime, int>>?> GetLast30DaysTransactionsChartData();
        Task<InvoiceResponseDTO?> Topup(PurchaseTopupDTO model);
    }
}
