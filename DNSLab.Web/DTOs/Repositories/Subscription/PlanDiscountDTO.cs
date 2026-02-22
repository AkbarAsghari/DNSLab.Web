using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Subscription
{
    public class PlanDiscountDTO
    {
        public int Id { get; set; }
        public int DiscountRate { get; set; }

        public PlanDurationDTO Duration { get; set; }
    }
}
