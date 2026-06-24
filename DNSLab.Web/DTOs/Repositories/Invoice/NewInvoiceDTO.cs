using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Invoice
{
    public class PurchaseDTO
    {
        public bool UseWallet { get; set; }
    }
    public class PurchaseSubscriptionDTO : PurchaseDTO
    {
        public int PlanId { get; set; }
        public int DiscountId { get; set; }
    }

    public class PurchaseAdditionalTrafficDTO : PurchaseDTO
    {
        public int Traffic { get; set; }
    }

    public class PurchaseTopupDTO
    {
        public int Amount { get; set; }
    }
}
