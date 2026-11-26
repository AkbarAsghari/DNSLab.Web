using DNSLab.Web.API.Common.Pagination;

namespace DNSLab.Web.API.DNS.DTOs;

public record RecordChangeRequest(
    Guid RecordId,
    PaginationRequest Pagination);
