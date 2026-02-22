using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Enums;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<PlanSectionDTO>?> GetPlans();
        Task<bool> Subscribe(int planId, int discountId);
        Task<IEnumerable<SubscriptionDTO>?> GetSubscribes();
        Task<IEnumerable<SubscriptionDTO>?> GetAllSubscribes();
        Task<bool> CheckSbscriptionFeature(FeatureEnum feature);
    }
}
