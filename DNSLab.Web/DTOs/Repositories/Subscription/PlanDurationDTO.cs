using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Subscription
{
    public class PlanDurationDTO
    {
        public int Id { get; set; }
        public int DurationInMonth { get; set; }
        public string Description { get; set; }
    }
}
