using DNSLab.Web.API.DNS.DDNS;
using DNSLab.Web.API.DNS.DTOs.Common;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.DynamicDNSs;

partial class RouterSettings
{
    [Inject] public IDDNSApiClient DDNSApiClient {  get; set; }

    List<ZoneAndRecordsResponse>? _AllRecords { get; set; }
    string? _SelectedHostName { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        var response  = await DDNSApiClient.GetZonesAndARecordsAsync();

        if (response.IsSuccess)
        {
            _AllRecords = response.Value;
        }
    }

}
