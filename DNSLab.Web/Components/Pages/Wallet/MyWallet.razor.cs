using ApexCharts;
using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.API.Wallet;
using DNSLab.Web.API.Wallet.DTOs;
using DNSLab.Web.Components.Dialogs.Wallet;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Wallet;

partial class MyWallet
{
    [Inject] IWalletApiClient WalletApiClient { get; set; }
    [Inject] IDialogService _DialogService { get; set; }

    WalletResponse? _Wallet { get; set; }
    PaginationResponse<WalletTransactionResponse>? _WalletTransactions { get; set; }

    List<WalletTransactionChartPoint> _Last30DaysTransactionsChartDataChartData { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var walletResponse = await WalletApiClient.GetWalletAsync();
        if (walletResponse.IsSuccess)
        {
            _Wallet = walletResponse.Value!;
        }

        var walletTransactionsResponse = await WalletApiClient.GetWalletTransactionsAsync(
            new PaginationRequest(Page: 1, PageSize: 5));
        if (walletTransactionsResponse.IsSuccess)
        {
            _WalletTransactions = walletTransactionsResponse.Value!;
        }

        var last30DaysTransactionsChartDataResponse = await WalletApiClient.GetLast30DaysTransactionsChartDataAsync();
        if (last30DaysTransactionsChartDataResponse.IsSuccess)
        {
            _Last30DaysTransactionsChartDataChartData = last30DaysTransactionsChartDataResponse.Value!;
        }
    }

    async Task IncreaseBalance()
    {
        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<IncreaseBalanceDialog>("افزایش موجودی", options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await OnInitializedAsync();
        }
    }

    private readonly ApexChartOptions<WalletTransactionChartPoint> _last30DaysTransactionsChartOptions = new()
    {
        Chart = new() { Background = "transparent", Toolbar = new() { Show = false }, Animations = new() { Enabled = true, Speed = 500 } },
        Theme = new() { Mode = Mode.Dark },
        Colors = new() { "var(--mud-palette-primary)" },
        Fill = new() { Opacity = 0.15 },
        Stroke = new() { Curve = Curve.Smooth, Width = 2 },
        Grid = new() { Show = false },
        Xaxis = new() { Labels = new() { Show = false }, AxisBorder = new() { Show = false }, AxisTicks = new() { Show = false } },
        Yaxis = new() { new() { Show = false } },
        DataLabels = new() { Enabled = false },
        Legend = new() { Show = false },
        Tooltip = new() { Enabled = true, Theme = Mode.Dark }
    };
}
