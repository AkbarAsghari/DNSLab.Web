using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class LoginWithPassword
{
    [Inject] IAccountRepository _AccountRepository { get; set; }
    [Inject] IAuthenticationProvider _AuthenticationProvider { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string RedirectTo { get; set; }
    protected override void OnInitialized()
    {
        RedirectTo = _NavigationManager.Uri.Substring(_NavigationManager.BaseUri.Length).ToLower();

        if (!RedirectTo.ToLower().EndsWith("login-with-password"))
            _Snackbar.Add("برای ادامه ابتدا باید وارد شوید", Severity.Info);
    }

    AuthenticateDTO _AuthenticateDTO = new();

    public async Task Authenticate()
    {
        var response = await _AccountRepository.AuthenticateAsync(_AuthenticateDTO);

        if (response != null)
        {
            await _AuthenticationProvider.Login(response);
            if (RedirectTo.ToLower().EndsWith("login-with-password"))
            {
                _NavigationManager.NavigateTo("/Dashboard");
            }
        }
    }
}
