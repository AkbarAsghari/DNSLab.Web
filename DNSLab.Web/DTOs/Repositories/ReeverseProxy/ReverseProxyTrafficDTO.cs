using DNSLab.Shared.Enums;
using DNSLab.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Shared.DTOs.ReverseProxy
{
    public class ReverseProxyTrafficDTO
    {
        public Guid Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long Traffic { get; set; }
        public long RemainTraffic { get; set; }
        public ReverseProxyTrafficTypeEnum Type { get; set; }
    }
}
