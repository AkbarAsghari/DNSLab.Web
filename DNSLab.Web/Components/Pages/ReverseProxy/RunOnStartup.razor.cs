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

    TunnelType _SelectedTunnelType = TunnelType.HTTP;

    bool _Minimize = false;
    bool _AddToken = false;

    int _Port = 8000;
    string _CustomDomain = String.Empty;
    int _CustomPort = 0;

    string _Script
    {
        get
        {
            string type = _SelectedTunnelType.ToString().ToLower();
            string customAddress = String.Empty;

            switch (_SelectedTunnelType)
            {
                case TunnelType.HTTP:
                    if (!String.IsNullOrWhiteSpace(_CustomDomain))
                    {
                        customAddress = $"{_CustomDomain.ToLower()}.http.dnslab.ir";
                    }
                    break;
                case TunnelType.TCP:
                    if (_CustomPort != 0)
                    {
                        customAddress = $"tcp.dnslab.ir:{_CustomPort}";
                    }
                    break;
                case TunnelType.UDP:
                    if (_CustomPort != 0)
                    {
                        customAddress = $"udp.dnslab.ir:{_CustomPort}";
                    }
                    break;
                default:
                    break;
            }

            return
                 $"{(_AddToken ? $"./dnslab.exe token set {_Token}\n" : String.Empty)}" +
                    $"""
                    @echo off
                    start{(_Minimize ? " /min" : String.Empty)} dnslab.exe {type} {_Port} {customAddress}
                    """;
        }
    }
}
