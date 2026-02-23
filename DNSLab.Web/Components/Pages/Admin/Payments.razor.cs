using ApexCharts;
using DNSLab.Web.DTOs.Repositories.Payment;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Subscription;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Admin;

partial class Payments
{
    [Inject] IPaymentRepository _PaymentRepository { get; set; }

    PagedResult<PaymentDTO>? _AllPayments;
    MudDataGrid<PaymentDTO> _Grid { get; set; }
    private async Task<GridData<PaymentDTO>> ServerReload(GridState<PaymentDTO> state)
    {
        PagedResult<PaymentDTO>? data = await _PaymentRepository.GetAllPayments(state.Page, state.PageSize);

        if (data is null)
        {
            return new GridData<PaymentDTO>();
        }

        var totalItems = data.RowCount;

        return new GridData<PaymentDTO>
        {
            TotalItems = totalItems,
            Items = data.Results
        };
    }
}
