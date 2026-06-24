using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Invoice
{
    public class InvoiceResponseDTO
    {
        public Guid InvoiceId { get; set; }
        public bool IsComplete { get; set; }
        public string? PaymentUrl { get; set; }
    }
}
