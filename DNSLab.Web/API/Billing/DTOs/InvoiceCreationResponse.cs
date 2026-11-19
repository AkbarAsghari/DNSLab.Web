namespace DNSLab.Web.API.Billing.DTOs;

public record InvoiceCreationResponse(
    Guid InvoiceId,
    bool IsComplete,
    string? PaymentUrl);
