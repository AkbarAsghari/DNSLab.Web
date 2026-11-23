using DNSLab.Web.API.Profile.DTOs;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Custom;

partial class UserIcon
{
    [Parameter] public UserBasicResponse UserInfo { get; set; }
    [Parameter] public int Size { get; set; } = 25;
}
