using DNSLab.Shared.DTOs.ReverseProxy;
using DNSLab.Web.Components.Dialogs.ReverseProxy;
using DNSLab.Web.Components.Dialogs.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DNSLab.Web.Components.Pages.ReverseProxy;

partial class Traffic
{
    [Inject] IReverseProxyTrafficRepository _ReverseProxyTrafficRepository { get; set; }
    [Inject] IDialogService _DialogService { get; set; }

    IEnumerable<ReverseProxyTrafficDTO>? _Traffics { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _Traffics = await _ReverseProxyTrafficRepository.GetCurrentTraffics();
    }

    async Task RechargeTraffic()
    {
        var options = new DialogOptions() { FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<AddonTrafficDialog>("ترافیک اضافه", options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await OnInitializedAsync();
        }
    }
}
