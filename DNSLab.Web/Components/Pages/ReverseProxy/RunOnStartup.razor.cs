using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.ReverseProxy;

partial class RunOnStartup
{
    [Inject] IReverseProxyRepository _ReverseProxyRepository { get; set; }

    string _Token;
    protected override async Task OnInitializedAsync()
    {
        _Token = await _ReverseProxyRepository.GetClientToken() ?? String.Empty;
    }
}
