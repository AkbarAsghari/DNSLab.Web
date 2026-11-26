using DNSLab.Web.API.Billing;
using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Pagination;
using DNSLab.Web.DTOs.Repositories.Payment;
using DNSLab.Web.DTOs.Repositories.Shared;
using DNSLab.Web.DTOs.Repositories.Wallet;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Payment;

partial class Payments
{
    [Inject] IPaymentApiClient PaymentApiClient { get; set; }

    private async Task<GridData<PaymentResponse>> ServerReload(GridState<PaymentResponse> state)
    {
        var response = await PaymentApiClient.GetUserPaymentsAsync( 
            new PaginationRequest(state.Page, state.PageSize));

        if (!response.IsSuccess)
        {
            return new GridData<PaymentResponse>();
        }
        PaginationResponse<PaymentResponse>? data = response.Value!;

        var totalItems = data.TotalCount;

        return new GridData<PaymentResponse>
        {
            TotalItems = totalItems,
            Items = data.Items
        };
    }
}
