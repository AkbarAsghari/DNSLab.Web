using DNSLab.Web.Components.Pages.Defaults;
using DNSLab.Web.DTOs.Repositories.Payment;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class PaymentRepository(IHttpServiceProvider _HttpServiceProvider) : IPaymentRepository
    {
        const string APIController = "Payment";

        public async Task<PagedResult<PaymentDTO>?> GetAllPayments(int page = 1, int pageSize = 10)
        {
            return await _HttpServiceProvider.Get<PagedResult<PaymentDTO>?>($"{APIController}/GetAllPayments?page={page}&pageSize={pageSize}");
        }

        public async Task<PagedResult<PaymentDTO>?> GetPayments(int page = 1, int pageSize = 10)
        {
            return await _HttpServiceProvider.Get<PagedResult<PaymentDTO>?>($"{APIController}/GetPayments?page={page}&pageSize={pageSize}");
        }

        public async Task<bool> Verify(long trackId)
        {
            return await _HttpServiceProvider.Get<bool>($"{APIController}/Verify?TrackId={trackId}");
        }

    }
}
