using DNSLab.Shared.DTOs.ReverseProxy;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class ReverseProxyTrafficRepository(IHttpServiceProvider _HttpServiceProvider) : IReverseProxyTrafficRepository
    {
        const string APIController = "ReverseProxyTraffic";

        public Task<IEnumerable<ReverseProxyTrafficDTO>?> GetCurrentTraffics()
        {
            return _HttpServiceProvider.Get<IEnumerable<ReverseProxyTrafficDTO>?>($"{APIController}/GetCurrentTraffics");
        }
    }
}
