namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IStaticRepository
    {
        Task<int?> GetZonesCount();
        Task<int?> GetRecordsCount();
        Task<int?> GetDDNSsCount();
        Task<int?> GetAllDDNSsCount();
        Task<int?> GetAllUsersCount();
        Task<int?> GetAllPremiumUsersCount();
        Task<int?> GetAllTodayChangesCount();
        Task<int?> OnlineTunnelsCount();
    }
}
