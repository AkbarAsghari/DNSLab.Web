using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.API.Zone.Enums;

public enum ZoneStatus
{
    [Display(Name = "در انتظار فعال‌سازی")]
    Pending = 1,

    [Display(Name = "فعال")]
    Active = 2,

    [Display(Name = "دارای هشدار")]
    Warning = 3,

    [Display(Name = "تعلیق‌شده")]
    Suspended = 4,

    [Display(Name = "منقضی‌شده")]
    Expired = 5,

    [Display(Name = "حذف‌شده")]
    Deleted = 6,
}
