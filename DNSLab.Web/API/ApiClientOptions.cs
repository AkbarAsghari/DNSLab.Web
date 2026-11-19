namespace DNSLab.Web.API;

public class ApiClientOptions
{
    public const string SectionName = "ApiClient";

    public string BaseAddress { get; set; } = string.Empty;

    public int Timeout { get; set; } = 30;
}
