namespace DNSLab.Web.API.Wallet.DTOs;

public sealed record WalletTransactionChartPoint(
    DateTime Date,
    long Amount);
