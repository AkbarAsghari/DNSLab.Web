using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.API.Page.Enums;

public enum PageType
{
    [Display(Name = "بلاگ")]
    Blog = 1,
    [Display(Name = "دانشنامه")]
    Wiki = 2,
}
