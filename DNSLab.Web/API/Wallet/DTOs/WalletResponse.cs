namespace DNSLab.Web.API.Wallet.DTOs;

public sealed record WalletResponse(
    Guid Id,
    long Balance);
