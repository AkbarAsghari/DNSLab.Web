using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Repositories;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class Information
{
    [Inject] IAccountRepository _AccountRepository { get; set; }
    [Inject] ISnackbar _Snackbar { get; set; }

    UserDTO? _CurrentUser;
    protected override async Task OnInitializedAsync()
    {
        _CurrentUser = await _AccountRepository.GetCurrentUserAsync();
    }

    async Task SaveInformation()
    {
        if (_CurrentUser != null)
        {
            await _AccountRepository.UpdateAsync(new UpdateUserPersonalInfoDTO
            {
                Company = _CurrentUser.Company,
                FirstName = _CurrentUser.FirstName,
                LastName = _CurrentUser.LastName,
            });
            _Snackbar.Add("تغییرات با موفقیت اعمال شد", Severity.Success);
        }
    }

    bool _IsConfirmEmailLinkResend = false;
    async Task ResendConfirmEmailLink()
    {
        _IsConfirmEmailLinkResend = await _AccountRepository.ResendConfirmEmailTokenAsync();
    }

    bool _EditMobileDialogVisible = false;
    void EditMobile()
    {
        _NewMobile = _CurrentUser!.Mobile;
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
            var token = await _AccountRepository.ChangeMobileAsync(_NewMobile);

            if (token is not null)
            {
                if (token == String.Empty)
                {
                    _CurrentUser!.Mobile = _NewMobile;
                    _NewMobile = String.Empty;
                    _EditMobileDialogVisible = false;
                }
                else
                {
                    _OtpToken = token;
                }
            }

        }
    }

    string _Otp;
    async Task ConfirmOtp()
    {
        if (await _AccountRepository.ConfirmOtpAsync(_OtpToken, _Otp))
        {
            _CurrentUser!.Mobile = _NewMobile;
            _NewMobile = String.Empty;
            _EditMobileDialogVisible = false;
            _OtpToken = String.Empty;
            _Otp = String.Empty;
            _EditMobileDialogVisible = false;
            _Snackbar.Add("شماره همراه شما ثبت شد", Severity.Success);
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
            if (await _AccountRepository.ChangePasswordAsync(_ChangePasswordDTO))
            {
                _ChangePasswordDTO = new();
                _EditPasswordDialogVisible = false;
            }
        }
    }

    bool _EditEmailDialogVisible = false;
    void EditEmail()
    {
        _NewEmailAddress = _CurrentUser!.Email;
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
            if (_NewEmailAddress.Equals(_CurrentUser!.Email))
            {
                _NewEmailAddress = String.Empty;
                _EditEmailDialogVisible = false;
                return;
            }

            if (await _AccountRepository.ChangeEmailAsync(_NewEmailAddress))
            {
                _CurrentUser!.Email = _NewEmailAddress;
                _NewEmailAddress = String.Empty;
                _EditEmailDialogVisible = false;
                await OnInitializedAsync();
            }
        }
    }

    bool _EditUsernameDialogVisible = false;
    void EditUsername()
    {
        _NewUsername = _CurrentUser!.Username;
        _EditUsernameDialogVisible = true;
    }
    string? _NewUsername;
    async Task SaveNewUsername()
    {
        if (_NewUsername!.Equals(_CurrentUser!.Username))
        {
            _NewUsername = String.Empty;
            _EditUsernameDialogVisible = false;
            return;
        }

        if (await _AccountRepository.UpdateUsernameAsync(_NewUsername))
        {
            _CurrentUser!.Username = _NewUsername;
            _NewUsername = String.Empty;
            _EditUsernameDialogVisible = false;
        }
    }
}
