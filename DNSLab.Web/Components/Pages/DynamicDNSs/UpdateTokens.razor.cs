using DNSLab.Web.API.DNS.DDNS;
using DNSLab.Web.API.DNS.DDNS.DTOs;
using DNSLab.Web.API.DNS.DDNS.Enums;
using DNSLab.Web.Components.Dialogs;
using DNSLab.Web.Components.Dialogs.DynamicDNSs;
using DNSLab.Web.Components.Dialogs.Record;
using DNSLab.Web.Components.Dialogs.Zone;
using DNSLab.Web.DTOs.Repositories.DDNS;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DNSLab.Web.Components.Pages.DynamicDNSs;

partial class UpdateTokens
{
    [Inject] IDDNSApiClient DDNSApiClient { get; set; }
    [Inject] IDialogService _DialogService { get; set; }
    [Inject] IJSRuntime _JSRuntime { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter] public bool IsSelectableList { get; set; }

    List<TokenResponse>? _Tokens { get; set; }

    bool _IsLoading = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _IsLoading = true;

            await Refresh();

            _IsLoading = false;
            await InvokeAsync(() => StateHasChanged());
        }
    }

    async Task Refresh()
    {
        var response = await DDNSApiClient.GetTokensAsync();
        if (response.IsSuccess)
        {
            _Tokens = response.Value;
        }
    }

    async Task NewToken()
    {
        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<UpdateTokenDialog>("اضافه کردن کلید جدید", options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await Refresh();
        }
    }

    async Task EditToken(TokenResponse token)
    {
        var response = await DDNSApiClient.GetTokenAsync(
            new TokenIdRequest(token.TokenId));

        if (!response.IsSuccess)
        {
            Snackbar.Add(response.Error!.Message, Severity.Warning);
        }

        var parameters = new DialogParameters<UpdateTokenDialog>() { { "Token", response.Value! } };

        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<UpdateTokenDialog>("ویرایش کلید", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            await Refresh();
        }
    }

    async Task DeleteToken(TokenResponse token)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال حذف {token.Name} میباشید آیا تایید میکنید؟" },
                { x => x.ButtonText, "حذف" },
                { x => x.Color, Color.Error }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("حذف", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            var response = await DDNSApiClient.DeleteTokenAsync(
                new TokenIdRequest(token.TokenId));

            if (response.IsSuccess)
            {
                Snackbar.Add("توکن با موفقیت حذف شد", Severity.Success);
                await Refresh();
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }
    }

    async Task RevokeTokenKey(TokenResponse token)
    {
        var parameters = new DialogParameters<BaseDialog>
            {
                { x => x.ContentText, $"شما در حال تغییر کلید {token.Name} میباشید آیا تایید میکنید؟ بعد از این عملیات دیگر امکان بروزرسانی با کلید قبلی وجود ندارد (کلید فعلی : {token.Key})" },
                { x => x.ButtonText, "تغییر کلید" },
                { x => x.Color, Color.Warning }
            };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small };

        var dialog = await _DialogService.ShowAsync<BaseDialog>("تغییر کلید", parameters, options);
        var result = await dialog.Result;
        if (!result!.Canceled)
        {
            var response = await DDNSApiClient.RevokeTokenKeyAsync(
               new TokenIdRequest(token.TokenId));

            if (response.IsSuccess)
            {
                Snackbar.Add("کلید توکن با موفقیت تغییر یافت", Severity.Success);
                await Refresh();
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }

        await OnTokenSelect.InvokeAsync(token);
    }

    async Task CopyLink(TokenResponse token)
    {
        await _JSRuntime.InvokeAsync<bool>("clipboardCopy.copyText", $"https://api.dnslab.link/DDNS/U?k={token.Key}");
    }

    async Task DownloadScript(Guid tokenId, ScriptType scriptType, CodeType codeType)
    {
        string fileName = String.Empty;
        byte[]? bytes = null;

        var response = await DDNSApiClient.GetTokenScriptAsync(new TokenScriptRequest(tokenId, scriptType, codeType));

        if (!response.IsSuccess)
        {
            Snackbar.Add(response.Error!.Message, Severity.Warning);
        }

        bytes = response.Value!.Data;

        switch (scriptType)
        {
            case ScriptType.Shell:
                fileName = "script.sh";
                break;
            case ScriptType.PowerShell:
                fileName = "script.ps1";
                break;
            default:
                break;
        }

        if (bytes != null)
        {
            await _JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, bytes);
        }
    }

    async Task SelectToken(TokenResponse token)
    {
        await OnTokenSelect.InvokeAsync(token);
    }

    [Parameter]
    public EventCallback<TokenResponse> OnTokenSelect { get; set; }

}
