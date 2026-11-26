using DNSLab.Web.API.DNS.DDNS;
using DNSLab.Web.API.DNS.DDNS.DTOs;
using DNSLab.Web.DTOs.Repositories.DDNS;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.DynamicDNSs;

public partial class RaspberryPi
{
    TokenResponse? _Token { get; set; } = null;
    void SelectToken(TokenResponse token)
    {
        _Token = token;
    }
}
