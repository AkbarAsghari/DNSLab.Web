using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class StaticRepository(IHttpServiceProvider _HttpServiceProvider) : IStaticRepository
    {
        const string APIController = "Static";

        public async Task<int?> GetAllDDNSsCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetAllDDNSsCount");
        }

        public async Task<int?> GetAllPremiumUsersCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetAllPremiumUsersCount");
        }

        public async Task<int?> GetAllTodayChangesCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetAllTodayChangesCount");
        }

        public async Task<int?> GetAllUsersCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetAllUsersCount");
        }

        public async Task<int?> GetDDNSsCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetDDNSsCount");
        }

        public async Task<int?> GetRecordsCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetRecordsCount");
        }

        public async Task<int?> GetZonesCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/GetZonesCount");
        }

        public async Task<int?> OnlineTunnelsCount()
        {
            return await _HttpServiceProvider.Get<int?>($"{APIController}/OnlineTunnelsCount");
        }
    }
}
