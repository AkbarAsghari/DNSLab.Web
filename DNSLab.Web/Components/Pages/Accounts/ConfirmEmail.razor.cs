﻿using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class ConfirmEmail
{
    [Inject] IAccountRepository _AccountRepository { get; set; }
    [Parameter] public string Token { get; set; }

    bool _IsSuccess = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsSuccess = await _AccountRepository.ConfirmEmailWithTokenAsync(Token);
            await InvokeAsync(StateHasChanged);
        }
    }
}
