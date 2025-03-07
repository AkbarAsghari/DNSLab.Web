﻿using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.Wallet;

partial class IncreaseBalanceDialog
{
    [Inject] IPaymentRepository _PaymentRepository { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    int _Amount = 50000; //Rial

    async Task Pay()
    {
        string? paymentUrl = await _PaymentRepository.RequestPaymentUrl(_Amount);

        if (!String.IsNullOrEmpty(paymentUrl))
        {
            _NavigationManager.NavigateTo(paymentUrl, true);
        }
    }
}
