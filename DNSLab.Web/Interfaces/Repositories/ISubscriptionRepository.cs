using DNSLab.Web.DTOs.Repositories.Invoice;
using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.Enums;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<PlanSectionDTO>?> GetPlans();
        Task<IEnumerable<SubscriptionDTO>?> GetSubscribes();
        Task<IEnumerable<SubscriptionDTO>?> GetAllSubscribes();
        Task<bool> CheckSbscriptionFeature(FeatureEnum feature);
        Task<InvoiceResponseDTO?> PurchaseSubscribe(PurchaseSubscriptionDTO model);
    }
}
