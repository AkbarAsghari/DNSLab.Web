using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Helpers;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class RegisterOrLogin
{
    [Inject] IAccountRepository _AccountRepository { get; set; }
    [Inject] IAuthenticationProvider _AuthenticationProvider { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string RedirectTo { get; set; }

    string _Mobile = String.Empty;
    string _Token = String.Empty;
    string _Otp = String.Empty;

    protected override void OnInitialized()
    {
        if (!String.IsNullOrEmpty(RedirectTo) && !RedirectTo.ToLower().EndsWith(AllRoutes.LoginWithPassword) && !RedirectTo.ToLower().EndsWith(AllRoutes.Login))
            _Snackbar.Add("برای ادامه ابتدا باید وارد شوید", Severity.Info);
    }

    public async Task RegisterUser()
    {
        var response = await _AccountRepository.RegisterOrAuthenticationAsync(_Mobile);

        if (response != null)
        {
            _Token = response;
        }
    }

    public async Task ConfirmOtp()
    {
        var response = await _AccountRepository.RegisterOrAuthenticationConfirmAsync(_Token, _Otp);

        if (response != null)
        {
            await _AuthenticationProvider.Login(response);
            if (String.IsNullOrEmpty(RedirectTo) || RedirectTo.ToLower().EndsWith(AllRoutes.LoginWithPassword) || RedirectTo.ToLower().EndsWith(AllRoutes.Login))
            {
                _NavigationManager.NavigateTo(AllRoutes.Dashboard);
            }
            else
            {
                _NavigationManager.NavigateTo(RedirectTo);
            }
        }
    }

    public async Task ResendOtp()
    {
        var token = await _AccountRepository.ResendOtp(_Token);
        if (token is not null)
        {
            _Token = token;
            _Otp = String.Empty;
        }
    }

    public async Task ChangeMobile()
    {
        _Token = String.Empty;
        _Otp = String.Empty;
    }
}
