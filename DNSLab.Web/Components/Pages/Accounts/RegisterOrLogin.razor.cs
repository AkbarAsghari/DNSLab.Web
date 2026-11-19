using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Helpers;
using DNSLab.Web.API.Authentication;
using DNSLab.Web.API.Authentication.DTOs;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class RegisterOrLogin
{
    [Inject] IAuthenticationApiClient authenticationApiClient { get; set; }
    [Inject] IAuthenticationProvider authenticationProvider { get; set; }
    [Inject] ISnackbar snackbar { get; set; }
    [Inject] NavigationManager navigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string RedirectTo { get; set; }

    string _Mobile = String.Empty;
    string _Token = String.Empty;
    string _Otp = String.Empty;

    protected override void OnInitialized()
    {
        if (!String.IsNullOrEmpty(RedirectTo) && !RedirectTo.ToLower().EndsWith(AllRoutes.LoginWithPassword) && !RedirectTo.ToLower().EndsWith(AllRoutes.Login))
            snackbar.Add("برای ادامه ابتدا باید وارد شوید", Severity.Info);
    }

    public async Task RegisterUser()
    {
        var response = await authenticationApiClient.StartOtpAuthenticationAsync(
            new StartOtpAuthenticationRequest(_Mobile));

        if (response.IsSuccess)
        {
            _Token = response.Value!;
        }
        else
        {
            snackbar.Add(response.Error!.Message, Severity.Warning);
        }
    }

    public async Task ConfirmOtp()
    {
        var response = await authenticationApiClient.ConfirmOtpAuthenticationAsync(
             new ConfirmOtpRequest(_Token, _Otp));

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

    public async Task ResendOtp()
    {
        var response = await authenticationApiClient.ResendOtpAsync(
             new ResendOtpRequest(_Token));

        if (response.IsSuccess)
        {
            _Token = response.Value!;
            _Otp = String.Empty;
        }
        else
        {
            snackbar.Add(response.Error!.Message, Severity.Warning);
        }
    }

    public async Task ChangeMobile()
    {
        _Token = String.Empty;
        _Otp = String.Empty;
    }
}
