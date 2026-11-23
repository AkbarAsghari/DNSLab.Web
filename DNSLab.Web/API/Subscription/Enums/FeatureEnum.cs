namespace DNSLab.Web.API.Subscription.Enums;

public enum FeatureEnum
{
    //DDNS
    RecordCount = 1,
    AbleToChangeTTL = 2,
    Monitoring = 3,
    PrivateZone = 9,
    RecordChangesHistory = 10,

    //Reverse Proxy
    ActiveTnnelsCount = 12,
    ReserveHTTPAddress = 17,
    ReservePort = 18,
    ActiveUdpSupport = 20,
    ReverseProxyClientTraffic = 21,
}
