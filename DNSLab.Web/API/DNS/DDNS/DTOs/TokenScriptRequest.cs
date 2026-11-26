using DNSLab.Web.API.DNS.DDNS.Enums;

namespace DNSLab.Web.API.DNS.DDNS.DTOs;

public record TokenScriptRequest(
    Guid TokenId,
    ScriptType ScriptType,
    CodeType CodeType);
