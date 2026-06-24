using DNSLab.Web.DTOs.Repositories.Payment;
using DNSLab.Web.DTOs.Repositories.Shared;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> Verify(long trackId);
        Task<PagedResult<PaymentDTO>?> GetPayments(int page = 1, int pageSize = 10);
        Task<PagedResult<PaymentDTO>?> GetAllPayments(int page = 1, int pageSize = 10);
    }
}
