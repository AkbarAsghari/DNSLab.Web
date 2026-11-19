using DNSLab.Web.API.Authentication.DTOs;

namespace DNSLab.Web.Interfaces.Providers
{
    public interface IAuthenticationProvider
    {
        Task Login(AuthenticateResponse model);
        Task Logout();
    }
}
