using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Subscription
{
    public class PlanDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BasePrice { get; set; }

        public IEnumerable<PlanDiscountDTO> Discounts { get; set; }
        public IEnumerable<FeatureDTO> Features { get; set; }
    }
}
