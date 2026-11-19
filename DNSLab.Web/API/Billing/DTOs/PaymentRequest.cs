namespace DNSLab.Web.API.Billing.DTOs;

public record PaymentRequest(
    long Amount,
    Guid? InvoiceId = null);