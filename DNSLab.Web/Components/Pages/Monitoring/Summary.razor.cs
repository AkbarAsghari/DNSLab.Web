using ApexCharts;
using DNSLab.Web.DTOs.Repositories.DNSLog;
using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.DTOs.Repositories.Zone;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Monitoring;

partial class Summary
{
    [Inject] IDDNSRepository _DDNSRepository { get; set; }
    [Inject] IDNSLogRepository _DNSLogRepository { get; set; }
    [Inject] ISubscriptionRepository _SubscriptionRepository { get; set; }

    bool? _IsSubscribeThisFeature { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        _IsSubscribeThisFeature = await _SubscriptionRepository.CheckSbscriptionFeature(Enums.FeatureEnum.Monitoring);
    }

    IEnumerable<Tuple<ZoneDTO, IEnumerable<BaseRecordDTO>>>? _AllRecords { get; set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _AllRecords = await _DDNSRepository.GetDDNSDomainAndRecordsForToken();
            await InvokeAsync(() => StateHasChanged());
        }
    }

    long? _TotalRequests { get; set; } = null;
    IEnumerable<TimeAndCountDTO>? _TimeAndCounts { get; set; }
    IEnumerable<ClientIPAndCountDTO>? _ClientIPAndCounts { get; set; }
    bool _IsLoading = false;
    string _LastSelectedQName = String.Empty;
    async Task QueryOnChange(string? qName = null)
    {
        _IsLoading = true;
        if (String.IsNullOrEmpty(qName))
        {
            qName = _LastSelectedQName;
        }
        _LastSelectedQName = qName;
        _TotalRequests = await _DNSLogRepository.GetTotalRequest(qName);
        _TimeAndCounts = await _DNSLogRepository.GetTotalRequestChartByTime(qName);
        _ClientIPAndCounts = await _DNSLogRepository.GetClientIPAndCounts(qName);
        _IsLoading = false;
    }

    private ApexChart<TimeAndCountDTO> _Chart;
    ApexChartOptions<TimeAndCountDTO> _LineOptions = new ApexChartOptions<TimeAndCountDTO>
    {
        Legend = new Legend
        {
            Position = LegendPosition.Bottom
        },
        Stroke = new Stroke { Curve = Curve.Smooth, Width = new ApexCharts.Size(1) },
        Tooltip = new Tooltip
        {
            Theme = Mode.Dark
        },
        Xaxis = new XAxis { Labels = new XAxisLabels { Rotate = 0 } },
        Grid = new Grid
        {
            Yaxis = new GridYAxis
            {
                Lines = new Lines
                {
                    Show = false,
                }
            }
        },
        Colors = new List<string> { "var(--mud-palette-primary)" },
        Chart = new Chart
        {
            Type = ApexCharts.ChartType.Bar,
            ForeColor = "var(--mud-palette-text-primary)",
            Background = "transparent",
            Toolbar = new Toolbar
            {
                Show = false,
            },
            Zoom = new Zoom
            {
                Enabled = false,
            },
            Height = "100%",
        },
    };

}
