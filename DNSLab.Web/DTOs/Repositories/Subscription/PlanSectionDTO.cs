using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Subscription
{
    public class PlanSectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PlanDTO> Plans { get; set; }
    }
}
