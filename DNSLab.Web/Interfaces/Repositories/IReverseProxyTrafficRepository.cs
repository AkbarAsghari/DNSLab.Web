using DNSLab.Shared.DTOs.ReverseProxy;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IReverseProxyTrafficRepository
    {
        Task<IEnumerable<ReverseProxyTrafficDTO>?> GetCurrentTraffics();
    }
}
