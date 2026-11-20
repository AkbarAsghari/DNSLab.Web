using DNSLab.Web.API.Page.Enums;

namespace DNSLab.Web.API.Page.DTOs;

public sealed record PageInfoResponse(
    Guid Id,
    string URL,
    string Title,
    string Description,
    bool IsPublished,
    DateTime? PublishDate,
    int VisitCount,
    PageType Type,
    DateTime CreateDate,
    DateTime? UpdateDate);
