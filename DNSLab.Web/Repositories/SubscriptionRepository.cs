using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class SubscriptionRepository(IHttpServiceProvider _HttpServiceProvider) : ISubscriptionRepository
    {
        const string APIController = "Subscription";

        public Task<bool> CheckSbscriptionFeature(FeatureEnum feature)
        {
            return _HttpServiceProvider.Get<bool>($"{APIController}/CheckSbscriptionFeature?feature={(int)feature}");
        }

        public Task<IEnumerable<SubscriptionDTO>?> GetAllSubscribes()
        {
            return _HttpServiceProvider.Get<IEnumerable<SubscriptionDTO>?>($"{APIController}/GetAllSubscribes");
        }

        public Task<IEnumerable<PlanSectionDTO>?> GetPlans()
        {
            return _HttpServiceProvider.Get<IEnumerable<PlanSectionDTO>?>($"{APIController}/GetPlans", false);
        }

        public Task<IEnumerable<SubscriptionDTO>?> GetSubscribes()
        {
            return _HttpServiceProvider.Get<IEnumerable<SubscriptionDTO>?>($"{APIController}/GetSubscribes");
        }

        public Task<bool> Subscribe(int planId, int discountId)
        {
            return _HttpServiceProvider.Post<bool>($"{APIController}/Subscribe?planId={planId}&discountId={discountId}");
        }
    }
}
