using DNSLab.Web.API.Billing.Enums;
using DNSLab.Web.API.Profile.DTOs;

namespace DNSLab.Web.API.Billing.DTOs;

public record AllPaymentResponse(
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
    DateTime CreateDate,
    UserBasicResponse User);