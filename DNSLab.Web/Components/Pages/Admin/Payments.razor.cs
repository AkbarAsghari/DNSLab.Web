using DNSLab.Web.API.Billing;
using DNSLab.Web.API.Billing.DTOs;
using DNSLab.Web.API.Common.Pagination;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Admin;

partial class Payments
{
    [Inject] IPaymentApiClient PaymentApiClient { get; set; }

    private async Task<GridData<AllPaymentResponse>> ServerReload(GridState<AllPaymentResponse> state)
    {
        var response = await PaymentApiClient.GetAllPaymentsAsync(
            new PaginationRequest(state.Page, state.PageSize));

        if (!response.IsSuccess)
        {
            return new GridData<AllPaymentResponse>();
        }
        PaginationResponse<AllPaymentResponse>? data = response.Value!;

        var totalItems = data.TotalCount;

        return new GridData<AllPaymentResponse>
        {
            TotalItems = totalItems,
            Items = data.Items
        };
    }
}
