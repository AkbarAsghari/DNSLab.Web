using ApexCharts;
using DNSLab.Web.API.Page;
using DNSLab.Web.API.Page.DTOs;
using DNSLab.Web.API.Page.Enums;
using DNSLab.Web.DTOs.Repositories.Page;
using DNSLab.Web.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using System;

namespace DNSLab.Web.Components.Pages.Pages;

partial class Page
{
    [Inject] IPageApiClient PageApiClient { get; set; }
    [Inject] public ISnackbar Snackbar { get; set; }

    [Parameter] public Guid PageId { get; set; } = Guid.Empty;

    List<PageType> _Types = new();

    protected override async Task OnInitializedAsync()
    {
        var response = await PageApiClient.GetPageTypesAsync();

        if (response.IsSuccess)
        {
            _Types = response.Value!;
        }

        if (PageId != Guid.Empty)
        {
            var pageResponse = await PageApiClient.GetPageAsync(
                new PageIdRequest(PageId));

            if (pageResponse.IsSuccess)
            {
                var page = pageResponse.Value!;

                _Id = page.Id;
                _Url = page.URL;
                _Title = page.Title;
                _Description = page.Description;
                _Body = page.Body;
                _IsPublished = page.IsPublished;
                _Type = page.Type;
            }
            else
            {
                Snackbar.Add(pageResponse.Error!.Message, Severity.Warning);
            }
        }
    }

    Guid _Id;
    string _Url;
    string _Title;
    string _Description;
    string _Body;
    bool _IsPublished;
    PageType _Type;

    public async Task Submit()
    {
        if (PageId == Guid.Empty)
        {
            var response = await PageApiClient.CreatePageAsync(
                new CreatePageRequest(
                    _Url,
                    _Title,
                    _Description,
                    _Body,
                    _IsPublished,
                    _Type));

            if (response.IsSuccess)
            {
                _Id = response.Value;
                Snackbar.Add("صفحه جدید با موفقیت ایجاد شد", Severity.Success);
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }
        else
        {
            var response = await PageApiClient.UpdatePageAsync(
                new UpdatePageRequest(
                    _Id,
                    _Url,
                    _Title,
                    _Description,
                    _Body,
                    _IsPublished,
                    _Type));

            if (response.IsSuccess)
            {
                Snackbar.Add("صفحه با موفقیت ویرایش شد", Severity.Success);
            }
            else
            {
                Snackbar.Add(response.Error!.Message, Severity.Warning);
            }
        }
    }
}
