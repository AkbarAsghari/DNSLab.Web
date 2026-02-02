using DNSLab.Shared.Enums;
using DNSLab.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSLab.Web.DTOs.Repositories.Bundle
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FeatureTypeEnum Type { get; set; }
        public int Order { get; set; }
        public FeaturePricingDTO Pricing { get; set; }
        public FeatureUnitDTO? Unit { get; set; }
    }
}
