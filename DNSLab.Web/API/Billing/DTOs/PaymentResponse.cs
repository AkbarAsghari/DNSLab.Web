using DNSLab.Web.API.Billing.Enums;

namespace DNSLab.Web.API.Billing.DTOs;

public record PaymentResponse(
    Guid PaymentId,
    long TrackId,
    long Amount,
    string? Description,
    string? OrderId,
    PaymentStatus Status,
    long? RefNumber,
    DateTime? PaidAt,
    DateTime? VerifiedAt,
    string? CardNumber,
    DateTime CreateDate);
    