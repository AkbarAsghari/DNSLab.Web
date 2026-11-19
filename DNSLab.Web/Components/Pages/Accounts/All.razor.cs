
using Microsoft.AspNetCore.Components;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.API.Profile;
using DNSLab.Web.API.Profile.DTOs;

namespace DNSLab.Web.Components.Pages.Accounts;

partial class All
{
    [Inject] IProfileApiClient ProfileApiClient { get; set; }

    List<UserProfileResponse> Profiles = new List<UserProfileResponse>();
    protected override async Task OnInitializedAsync()
    {
        var responnse = await ProfileApiClient.GetAllProfilesAsync();

        if (responnse.IsSuccess)
        {
            Profiles = responnse.Value!;
        }
    }
}
