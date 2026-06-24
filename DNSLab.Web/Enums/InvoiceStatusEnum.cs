using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.Enums
{
    public enum InvoiceStatusEnum
    {
        Draft = 1,
        Pending = 2,
        Processing = 3,
        Paid = 4,
        Failed = 5,
        Cancelled = 6,
        Expired = 7,
    }
}
