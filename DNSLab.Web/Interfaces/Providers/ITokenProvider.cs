using DNSLab.Web.API.Authentication.DTOs;

namespace DNSLab.Web.Interfaces.Providers
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
        Task SetTokenAsync(AuthenticateResponse model);
        Task DeleteTokenAsync();
    }
}
