using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.API.Subscription.Enums;

public enum FeatureUnit
{
    [Display(Name = "عدد")]
    Count = 1,
    [Display(Name = "گیگابایت")]
    Traffic = 2,
    None = 3
}
