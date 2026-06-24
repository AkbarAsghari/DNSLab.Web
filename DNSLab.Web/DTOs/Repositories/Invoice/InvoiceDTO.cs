using DNSLab.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Invoice
{
    public class InvoiceDTO
    {
        public Guid Id { get; set; }
        public InvoiceTypeEnum Type { get; set; }
        public InvoiceStatusEnum Status { get; set; }
        public int Number { get; set; }
        public long TotalAmount { get; set; }
        public long DiscountAmount { get; set; }
        public long GatewayAmount { get; set; }
        public bool UseWalletAmount { get; set; }
        public DateTime? ExpireAt { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
