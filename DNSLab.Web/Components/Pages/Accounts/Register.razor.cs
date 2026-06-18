using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class Register
{
    [Inject] IAccountRepository _AccountRepository { get; set; }
    [Inject] IAuthenticationProvider _AuthenticationProvider { get; set; }
    [Inject] ISnackbar _Snachbar { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    string _Mobile = String.Empty;
    string _Token = String.Empty;
    string _Otp = String.Empty;

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
            _NavigationManager.NavigateTo("/Dashboard");
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
}
