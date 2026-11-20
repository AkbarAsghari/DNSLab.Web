using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.API.Ticket.Enums;

public enum TicketStatus
{
    [Display(Name = "بررسی نشده")]
    Unreviewed = 1,
    [Display(Name = "در حال بررسی")]
    InProgress = 2,
    [Display(Name = "پاسخ داده شده")]
    Answered = 3
}
