using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Coupon
{
    public class CouponDTO
    {
        public Guid Id { get; set; }
        public long DiscountAmount { get; set; }
    }
}
