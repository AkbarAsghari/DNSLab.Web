using DNSLab.Web.DTOs.Repositories.Record;
using DNSLab.Web.Enums;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace DNSLab.Web.Components.Pages.Tools;

partial class DNSLookup
{
    [Inject] IDNSClientRepository _DNSClientRepository { get; set; }
    [Inject] NavigationManager _NavigationManager { get; set; }

    [Parameter, SupplyParameterFromQuery]
    public string? type
    {
        get
        {
            return _QueryType.ToString();
        }
        set
        {
            if (!Enum.TryParse(value, out _QueryType))
            {
                _QueryType = QueryTypeEnum.A;
            }
        }
    }
    [Parameter, SupplyParameterFromQuery]
    public string query { get; set; } = String.Empty;

    QueryTypeEnum _QueryType;
    string _NameServer = "217.218.127.127";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (!String.IsNullOrEmpty(query) && !String.IsNullOrEmpty(type))
            {
                await Query();
                await InvokeAsync(() => StateHasChanged());
            }
        }
    }

    string? _Result { get; set; }
    public async Task Query()
    {
        _NavigationManager.NavigateTo($"Tools/DNSLookup?Type={type}&Query={query}", options: new NavigationOptions
        {
            ReplaceHistoryEntry = true
        });

        _Result = await _DNSClientRepository.Query(_QueryType, query, _NameServer);
    }
}
