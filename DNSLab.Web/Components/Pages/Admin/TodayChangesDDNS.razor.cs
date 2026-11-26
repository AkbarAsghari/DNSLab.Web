using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;
using DNSLab.Web.DTOs.Repositories.Zone;
using static MudBlazor.CategoryTypes;
using System.Net.Http;
using DNSLab.Web.API.DNS.DDNS;
using DNSLab.Web.API.DNS.DTOs.Common;

namespace DNSLab.Web.Components.Pages.Admin
{
    partial class TodayChangesDDNS
    {
        [Inject] IDDNSApiClient DDNSApiClient { get; set; }

        List<ZoneAndRecordsResponse>? _ZonesAndRecords { get; set; } = default;

        protected override async Task OnInitializedAsync()
        {
            var response = await DDNSApiClient.GetTodayChangedZonesWithDDNSRecordsAsync();
            if (response.IsSuccess)
            {
                _ZonesAndRecords = response.Value;
            }
        }
    }
}
