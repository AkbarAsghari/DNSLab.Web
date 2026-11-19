using DNSLab.Web.API.Wallet.Enums;

namespace DNSLab.Web.API.Wallet.DTOs;

public sealed record WalletTransactionResponse(
    Guid Id,
    long Amount,
    string? Description,
    WalletTransactionType TransactionType,
    WalletTransactionStatus? Status,
    DateTime CreateDate);
