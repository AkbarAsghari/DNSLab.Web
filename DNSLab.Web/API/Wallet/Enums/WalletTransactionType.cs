using System.ComponentModel.DataAnnotations;

namespace DNSLab.Web.API.Wallet.Enums;

public enum WalletTransactionType
{

    [Display(Name = "واریز")]
    Deposit = 1,

    [Display(Name = "برداشت")]
    Withdrawal = 2
}
