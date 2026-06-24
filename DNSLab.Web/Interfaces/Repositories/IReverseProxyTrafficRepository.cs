using DNSLab.Shared.DTOs.ReverseProxy;
using DNSLab.Web.DTOs.Repositories.Invoice;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IReverseProxyTrafficRepository
    {
        Task<IEnumerable<ReverseProxyTrafficDTO>?> GetCurrentTraffics();
        Task<InvoiceResponseDTO?> PurchaseAdditionalTraffic(PurchaseAdditionalTrafficDTO model);
    }
}
