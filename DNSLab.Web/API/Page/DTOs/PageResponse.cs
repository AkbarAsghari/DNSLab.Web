using DNSLab.Domain.Content.Enums;

namespace DNSLab.Web.API.Page.DTOs;

public sealed record PageResponse(
    Guid Id,
    Guid UserId,
    string URL,
    string Title,
    string Description,
    string Body,
    bool IsPublished,
    DateTime? PublishDate,
    int VisitCount,
    PageType Type,
    DateTime CreateDate);
