namespace DNSLab.Web.API.Billing.Enums;

public enum PaymentStatus
{
    Pending = -1,
    InternalError = -2,
    PaidVerified = 1,
    PaidNotVerified = 2,
    CancelledByUser = 3,
    InvalidCardNumber = 4,
    InsufficientBalance = 5,
    InvalidPassword = 6,
    TooManyRequests = 7,
    DailyOnlinePaymentLimitExceeded = 8,
    DailyOnlinePaymentAmountExceeded = 9,
    InvalidCardIssuer = 10,
    SwitchError = 11,
    CardUnavailable = 12
}
