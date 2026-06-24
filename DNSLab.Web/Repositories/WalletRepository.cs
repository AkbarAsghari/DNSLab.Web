using DNSLab.Web.DTOs.Repositories.Invoice;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using System.Reflection;

namespace DNSLab.Web.Repositories
{
    public class WalletRepository(IHttpServiceProvider _HttpServiceProvider) : IWalletRepository
    {
        const string APIController = "Wallet";

        public async Task<IEnumerable<Tuple<DateTime, int>>?> GetLast30DaysTransactionsChartData()
        {
            return await _HttpServiceProvider.Get<IEnumerable<Tuple<DateTime, int>>?>($"{APIController}/GetLast30DaysTransactionsChartData");
        }

        public async Task<WalletDTO?> GetWallet()
        {
            return await _HttpServiceProvider.Get<WalletDTO?>($"{APIController}/GetWallet");
        }

        public async Task<PagedResult<WalletTransactionDTO>?> GetWalletTransactions(int page = 1, int pageSize = 10)
        {
            return await _HttpServiceProvider.Get<PagedResult<WalletTransactionDTO>?>($"{APIController}/GetWalletTransactions?page={page}&pageSize={pageSize}");
        }

        public async Task<InvoiceResponseDTO?> Topup(PurchaseTopupDTO model)
        {
            return await _HttpServiceProvider.Post<PurchaseTopupDTO, InvoiceResponseDTO?>($"{APIController}/Topup", model);
        }
    }
}
