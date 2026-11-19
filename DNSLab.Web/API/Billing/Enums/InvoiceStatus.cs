namespace DNSLab.Web.API.Billing.Enums;

public enum InvoiceStatus
{
    Created = 1,
    ReadyForPayment = 2,
    ProcessingPayment = 3,
    Paid = 4,
    PaymentFailed = 5,
    CancelledByUser = 6,
    PaymentExpired = 7
}
