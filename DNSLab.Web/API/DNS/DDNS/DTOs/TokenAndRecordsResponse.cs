using DNSLab.Web.API.DNS.DTOs.Common;

namespace DNSLab.Web.API.DNS.DDNS.DTOs;

public record TokenAndRecordsResponse(
  Guid TokenId,
  string Name,
  List<RecordResponse> Records);
