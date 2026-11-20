using DNSLab.Domain.Content.Enums;

namespace DNSLab.Web.API.Page.DTOs;

public sealed record CreatePageRequest(
    string URL,
    string Title,
    string Description,
    string Body,
    bool IsPublished,
    PageType Type);
