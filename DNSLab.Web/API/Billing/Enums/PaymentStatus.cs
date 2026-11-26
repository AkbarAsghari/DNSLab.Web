using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.API.Billing.Enums;

public enum PaymentStatus
{
    [Display(Name = "منتظر پرداخت")]
    Pending = -1,

    [Display(Name = "خطای داخلی")]
    InternalError = -2,

    [Display(Name = "پرداخت تأیید شده")]
    PaidVerified = 1,

    [Display(Name = "پرداخت تأیید نشده")]
    PaidNotVerified = 2,

    [Display(Name = "لغو پرداخت توسط کاربر")]
    CancelledByUser = 3,

    [Display(Name = "شماره کارت نامعتبر")]
    InvalidCardNumber = 4,

    [Display(Name = "موجودی حساب کافی نیست")]
    InsufficientBalance = 5,

    [Display(Name = "رمز عبور نامعتبر")]
    InvalidPassword = 6,

    [Display(Name = "تعداد درخواست‌ها بیش از حد مجاز")]
    TooManyRequests = 7,

    [Display(Name = "عبور از سقف تعداد تراکنش‌های روزانه پرداخت اینترنتی")]
    DailyOnlinePaymentLimitExceeded = 8,

    [Display(Name = "عبور از سقف مبلغ تراکنش‌های روزانه پرداخت اینترنتی")]
    DailyOnlinePaymentAmountExceeded = 9,

    [Display(Name = "صادرکننده کارت نامعتبر")]
    InvalidCardIssuer = 10,

    [Display(Name = "خطا در سوئیچ بانکی")]
    SwitchError = 11,

    [Display(Name = "کارت غیرقابل استفاده")]
    CardUnavailable = 12
}
