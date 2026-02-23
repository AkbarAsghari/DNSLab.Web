namespace DNSLab.Web.DTOs.Repositories.Subscription
{
    public class SubscriptionDTO
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public long TotalPrice { get; set; }

        public PlanSectionDTO Section { get; set; }
        public UserInfoDTO? User { get; set; }
        public PlanDTO Plan { get; set; }
        public PlanDurationDTO Duration { get; set; }
    }
}
