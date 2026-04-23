using System.Reflection;
using DNSLab.Web.DTOs.Repositories.Accounts;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Repositories
{
    public class AccountRepository(IHttpServiceProvider _HttpServiceProvider) : IAccountRepository
    {
        const string APIController = "Account";
        public async Task<AuthUserDTO?> AuthenticateAsync(AuthenticateDTO model)
        {
            return await _HttpServiceProvider.Post<AuthenticateDTO, AuthUserDTO>($"{APIController}/AuthenticateAsync", model);
        }

        public async Task<AuthUserDTO?> GenerateTokenWithRefreshTokenAsync(AuthUserDTO model)
        {
            return await _HttpServiceProvider.Post<AuthUserDTO, AuthUserDTO>($"{APIController}/GenerateTokenWithRefreshTokenAsync", model);
        }

        public async Task<bool> ChangeEmailAsync(string email)
        {
            return await _HttpServiceProvider.Put<bool>($"{APIController}/ChangeEmailAsync?email={email}");
        }

        public async Task<string?> ChangeMobileAsync(string mobile)
        {
            return await _HttpServiceProvider.Put<string?>($"{APIController}/ChangeMobileAsync?mobile={mobile}");
        }

        public async Task<bool> ConfirmOtpAsync(string token,string otp)
        {
            return await _HttpServiceProvider.Post<bool>($"{APIController}/ConfirmOtpAsync?token={token}&otp={otp}");
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO model)
        {
            return await _HttpServiceProvider.Put<ChangePasswordDTO, bool>($"{APIController}/ChangePasswordAsync", model);
        }

        public Task<bool> ChangeUserRoleAsync(Guid userId, RolesEnum role)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ConfirmEmailWithTokenAsync(string token)
        {
            return await _HttpServiceProvider.Post<bool>($"{APIController}/ConfirmEmailWithTokenAsync?token={token}");
        }

        public async Task<bool> DeactivateAccountAsync()
        {
            return await _HttpServiceProvider.Delete<bool>($"{APIController}/");

        }

        public async Task<bool> ForgetPasswordAsync(ForgetPasswordDTO model)
        {
            return await _HttpServiceProvider.Post<ForgetPasswordDTO, bool>($"{APIController}/ForgetPasswordAsync", model);
        }

        public async Task<IEnumerable<UserDTO>?> GetAllAsync()
        {
            return await _HttpServiceProvider.Get<IEnumerable<UserDTO>>($"{APIController}/GetAllAsync");
        }

        public async Task<UserDTO?> GetCurrentUserAsync()
        {
            return await _HttpServiceProvider.Get<UserDTO>($"{APIController}/GetCurrentUserAsync");
        }

        public async Task<UserDTO?> GetUserAsync(Guid userId)
        {
            return await _HttpServiceProvider.Get<UserDTO>($"{APIController}/");
        }

        public async Task<AuthUserDTO?> RegisterAsync(RegisterUserDTO model)
        {
            return await _HttpServiceProvider.Post<RegisterUserDTO, AuthUserDTO>($"{APIController}/RegisterAsync", model);
        }

        public async Task<bool> ResendConfirmEmailTokenAsync()
        {
            return await _HttpServiceProvider.Post<bool>($"{APIController}/ResendConfirmEmailTokenAsync");
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO model)
        {
            return await _HttpServiceProvider.Post<ResetPasswordDTO, bool>($"{APIController}/ResetPasswordAsync", model);
        }

        public async Task<bool> UpdateAsync(UpdateUserPersonalInfoDTO model)
        {
            return await _HttpServiceProvider.Put<UpdateUserPersonalInfoDTO, bool>($"{APIController}/UpdateAsync", model);
        }

        public async Task<bool> UpdateUsernameAsync(string? username)
        {
            return await _HttpServiceProvider.Put<bool>($"{APIController}/UpdateUsernameAsync?username={username}");
        }

        public async Task<int> UsersCountAsync()
        {
            return await _HttpServiceProvider.Get<int>($"{APIController}/");
        }
    }
}
