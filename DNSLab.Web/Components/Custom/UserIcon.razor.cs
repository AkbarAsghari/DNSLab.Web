using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Custom;

partial class UserIcon
{
    [Parameter] public UserInfoDTO UserInfo { get; set; }
    [Parameter] public int Size { get; set; } = 25;
}
