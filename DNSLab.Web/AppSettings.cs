﻿using DNSLab.Web.Enums;

namespace DNSLab.Web.AppSettings;

public sealed class GlobalSettings
{
    public const string ApplicationName = "DNSLab";
    public const bool RightToLeft = true;
    public const bool DarkMode = true;
    public const string APIBaseAddress = "https://localhost:7046/";
}

public static class DNSSettings
{
    public static List<RecordTypeEnum> SupportedRecordForDomain = new List<RecordTypeEnum>
    {
        RecordTypeEnum.A,
        RecordTypeEnum.AAAA,
        RecordTypeEnum.CNAME,
        RecordTypeEnum.TXT,
        RecordTypeEnum.MX,
    };

    public static List<RecordTypeEnum> SupportedRecordForDDNS = new List<RecordTypeEnum>
    {
        RecordTypeEnum.A,
        RecordTypeEnum.CNAME
    };
}

public sealed class AuthorizeRoles
{
    public const string Admin = "Admin";
    public const string Writer = "Writer";
    public const string User = "User";
}