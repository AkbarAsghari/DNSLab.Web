using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Dialogs.ReverseProxy;

partial class AddonTrafficDialog
{
    [Inject] ISubscriptionRepository _SubscriptionRepository { get; set; }
    [CascadingParameter] IMudDialogInstance _MudDialog { get; set; }

    int _Traffic = 10;

    async Task Pay()
    {
        if (await _SubscriptionRepository.RechargeTraffic(_Traffic))
        {
            _MudDialog.Close();
        }
    }
}
