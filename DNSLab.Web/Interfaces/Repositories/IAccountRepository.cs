using DNSLab.Web.DTOs.Repositories.Accounts;
using DNSLab.Web.Enums;

namespace DNSLab.Web.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<AuthUserDTO?> AuthenticateAsync(AuthenticateDTO model);
        Task<AuthUserDTO?> GenerateTokenWithRefreshTokenAsync(AuthUserDTO model);
        Task<bool> ForgetPasswordAsync(ForgetPasswordDTO model);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO model);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO model);
        Task<bool> ChangeUserRoleAsync(Guid userId, RolesEnum role);
        Task<AuthUserDTO?> RegisterAsync(RegisterUserDTO model);
        Task<bool> ConfirmEmailWithTokenAsync(string token);
        Task<bool> ResendConfirmEmailTokenAsync();
        Task<bool> UpdateAsync(UpdateUserPersonalInfoDTO model);
        Task<bool> UpdateUsernameAsync(string? username);
        Task<bool> ChangeEmailAsync(string email);
        Task<string?> ChangeMobileAsync(string mobile);
        Task<bool> ConfirmOtpAsync(string token, string otp);
        Task<bool> IsMobileApproved();
        Task<UserDTO?> GetCurrentUserAsync();
        Task<int> UsersCountAsync();
        Task<UserDTO?> GetUserAsync(Guid userId);
        Task<IEnumerable<UserDTO>?> GetAllAsync();
        Task<bool> DeactivateAccountAsync();
    }
}
