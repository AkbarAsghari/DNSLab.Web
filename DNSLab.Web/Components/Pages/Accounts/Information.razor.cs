using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.API.Profile;
using DNSLab.Web.API.Profile.DTOs;
using DNSLab.Web.API.Authentication;
using DNSLab.Web.API.Authentication.DTOs;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class Information
{
    [Inject] IProfileApiClient profileApiClient { get; set; }
    [Inject] IAuthenticationApiClient authenticationApiClient { get; set; }
    [Inject] ISnackbar snackbar { get; set; }

    UserProfileResponse? profile;
    string? _FirstName;
    string? _LastName;
    string? _Company;

    protected override async Task OnInitializedAsync()
    {
        var response = await profileApiClient.GetProfileAsync();

        if (response.IsSuccess)
        {
            profile = response.Value!;
            _FirstName = profile.FirstName;
            _LastName = profile.LastName;
            _Company = profile.Company;
        }
    }

    async Task SaveInformation()
    {
        if (profile != null)
        {
            var response = await profileApiClient.UpdateProfileAsync(
                new UpdateProfileRequest(
                    _FirstName ?? String.Empty,
                    _LastName ?? String.Empty,
                    _Company ?? String.Empty));

            if (response.IsSuccess)
            {
                snackbar.Add("تغییرات با موفقیت اعمال شد", Severity.Success);
            }
            else
            {
                snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }
    }

    bool _IsConfirmEmailLinkResend = false;
    async Task ResendConfirmEmailLink()
    {
        //_IsConfirmEmailLinkResend = await profileApiClient.emai.ResendConfirmEmailTokenAsync();
    }

    bool _EditMobileDialogVisible = false;
    void EditMobile()
    {
        _NewMobile = profile!.Mobile;
        _EditMobileDialogVisible = true;
    }

    MudForm _EditMobileForm;
    string _NewMobile;
    string _OtpToken;
    async Task SaveNewMobile()
    {
        _OtpToken = String.Empty;
        await _EditMobileForm.Validate();
        if (_EditMobileForm.IsValid)
        {
            var response = await profileApiClient.UpdateMobileAsync(new UpdateMobileRequest(_NewMobile));
            if (response.IsSuccess)
            {
                if (String.IsNullOrEmpty(response.Value))
                {
                    await OnInitializedAsync();
                    _NewMobile = String.Empty;
                    _EditMobileDialogVisible = false;
                }
                else
                {
                    _OtpToken = response.Value;
                }
            }
        }
    }

    async Task ResendOtp()
    {
        var resendOtpResponse = await authenticationApiClient.ResendOtpAsync(
            new ResendOtpRequest(_OtpToken));

        if (resendOtpResponse.IsSuccess)
        {
            _OtpToken = resendOtpResponse.Value!;
        }
        else
        {
            snackbar.Add(resendOtpResponse.Error!.Message, Severity.Warning);
        }
    }

    string _Otp;
    async Task ConfirmOtp()
    {
        var confirmOtpAuthenticationResponse = await authenticationApiClient.ConfirmOtpAuthenticationAsync(
            new ConfirmOtpRequest(_OtpToken, _Otp));

        if (confirmOtpAuthenticationResponse.IsSuccess)
        {
            await OnInitializedAsync();

            _NewMobile = String.Empty;
            _EditMobileDialogVisible = false;
            _OtpToken = String.Empty;
            _Otp = String.Empty;
            _EditMobileDialogVisible = false;
            snackbar.Add("شماره همراه شما ثبت شد", Severity.Success);
            await OnInitializedAsync();
        }
    }



    bool _EditPasswordDialogVisible = false;
    void EditPassword()
    {
        _EditPasswordDialogVisible = true;
    }
    MudForm _EditPasswordForm;
    ChangePasswordDTO _ChangePasswordDTO = new();
    async Task SaveNewPassword()
    {
        await _EditPasswordForm.Validate();
        if (_EditPasswordForm.IsValid)
        {
            var changePasswordResponse = await profileApiClient.UpdatePasswordAsync(
                new UpdatePasswordRequest(_ChangePasswordDTO.CurrentPassword, _ChangePasswordDTO.NewPassword));

            if (changePasswordResponse.IsSuccess)
            {
                _ChangePasswordDTO = new();
                _EditPasswordDialogVisible = false;
                snackbar.Add("رمز عبور شما تغییر یافت", Severity.Success);
            }
            else
            {
                snackbar.Add(changePasswordResponse.Error!.Message, Severity.Warning);
            }
        }
    }

    bool _EditEmailDialogVisible = false;
    void EditEmail()
    {
        _NewEmailAddress = profile!.Email;
        _EditEmailDialogVisible = true;
        _IsConfirmEmailLinkResend = true;
    }
    MudForm _EditEmailForm;
    string _NewEmailAddress;
    async Task SaveNewEmail()
    {
        await _EditEmailForm.Validate();
        if (_EditEmailForm.IsValid)
        {
            if (_NewEmailAddress.Equals(profile!.Email))
            {
                _NewEmailAddress = String.Empty;
                _EditEmailDialogVisible = false;
                return;
            }

            var updateEmailResponse = await profileApiClient.UpdateEmailAsync(
                new UpdateEmailRequest(_NewEmailAddress));

            if (updateEmailResponse.IsSuccess)
            {
                _NewEmailAddress = String.Empty;
                _EditEmailDialogVisible = false;
                await OnInitializedAsync();
            }
            else
            {
                snackbar.Add(updateEmailResponse.Error!.Message, Severity.Warning);
            }
        }
    }

    bool _EditUsernameDialogVisible = false;
    void EditUsername()
    {
        _NewUsername = profile!.Username;
        _EditUsernameDialogVisible = true;
    }
    string? _NewUsername;
    async Task SaveNewUsername()
    {
        if (_NewUsername!.Equals(profile!.Username))
        {
            _NewUsername = String.Empty;
            _EditUsernameDialogVisible = false;
            return;
        }

        var updateUsernameResponse = await profileApiClient.UpdateUsernameAsync(
            new UpdateUsernameRequest(_NewUsername));

        if (updateUsernameResponse.IsSuccess)
        {
            _NewUsername = String.Empty;
            _EditUsernameDialogVisible = false;
            await OnInitializedAsync();
        }
        else
        {
            snackbar.Add(updateUsernameResponse.Error!.Message, Severity.Warning);
        }
    }
}
