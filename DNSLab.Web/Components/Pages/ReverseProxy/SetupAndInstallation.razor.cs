using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace DNSLab.Web.Components.Pages.ReverseProxy;

partial class SetupAndInstallation
{
    [Inject] AuthenticationStateProvider _AuthenticationStateProvider { get; set; }
    [Inject] IReverseProxyRepository _ReverseProxyRepository { get; set; }

    public enum OS
    {
        Windows,
        Linux,
    }

    OS _SelectedOs = OS.Windows;

    void ChangeOS(OS selectedOS)
    {
        _SelectedOs = selectedOS;
        StateHasChanged();
    }

    string _Token = "XXXXXXX-XX-XXXXXXXX";

    protected override async Task OnInitializedAsync()
    {
        var authState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity != null && authState.User.Identity.IsAuthenticated)
        {
            _Token = await _ReverseProxyRepository.GetClientToken() ?? String.Empty;
        }
        else
        {
            _Token = "[برای دریافت کلید لطفا وارد حساب کاربری خود شوید]";
        }
    }
}
