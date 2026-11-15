namespace DNSLab.Web.API.Common.Result;

public record Error(
    string Code,
    string Message);