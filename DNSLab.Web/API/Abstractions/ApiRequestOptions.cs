namespace DNSLab.Web.API.Abstractions;

public sealed record ApiRequestOptions
{
    public bool RequiresAuthentication { get; init; } = true;
}