using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Helpers;
using DNSLab.Web.API.Authentication;
using DNSLab.Web.API.Authentication.DTOs;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class LoginWithPassword
{
    [Inject] IAuthenticationApiClient authenticationApiClient { get; set; }
    [Inject] IAuthenticationProvider authenticationProvider { get; set; }
    [Inject] ISnackbar snackbar { get; set; }
    [Inject] NavigationManager navigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string RedirectTo { get; set; }

    protected override void OnInitialized()
    {
        if (!String.IsNullOrEmpty(RedirectTo) && !RedirectTo.ToLower().EndsWith(AllRoutes.LoginWithPassword) && !RedirectTo.ToLower().EndsWith(AllRoutes.Login))
            snackbar.Add("برای ادامه ابتدا باید وارد شوید", Severity.Info);
    }

    AuthenticateDTO _AuthenticateDTO = new();

    public async Task Authenticate()
    {
        var response = await authenticationApiClient.AuthenticateAsync(
            new AuthenticateRequest(
                _AuthenticateDTO.MobileOrEmailOrUsername,
                _AuthenticateDTO.Password));

        if (response.IsSuccess)
        {
            await authenticationProvider.Login(response.Value!);
            if (String.IsNullOrEmpty(RedirectTo) || RedirectTo.ToLower().EndsWith(AllRoutes.LoginWithPassword) || RedirectTo.ToLower().EndsWith(AllRoutes.Login))
            {
                navigationManager.NavigateTo(AllRoutes.Dashboard);
            }
            else
            {
                navigationManager.NavigateTo(RedirectTo);
            }
        }
        else
        {
            snackbar.Add(response.Error!.Message, Severity.Warning);
        }
    }
}
