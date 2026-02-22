namespace DNSLab.Web.Helpers
{
    public sealed class AllRoutes
    {
        public const string Home = "/";

        public const string ItIsUnderDevelopment = "/it-is-under-development";

        public const string Dashboard = "/dashboard";
        public const string About = "/about";
        public const string Terms = "/terms";
        public const string Contribute = "/contribute";

        public const string AllZones = "/zones/all";
        public const string AllRecords = "/records";
        public const string RecordsHistory = "/records/history";

        public const string AllTools = "/tools/all";
        public const string DNSLookup = "/tools/dnslookup";
        public const string Ping = "/tools/ping";
        public const string ReverseLookup = "/tools/reverse-lookup";
        public const string PortChecker = "/tools/port-checker";

        public const string DDNSHostNames = "/ddns/hostnames";
        public const string UpdateTokens = "/ddns/update-tokens";
        public const string RouterSettings = "/ddns/router-settings";
        public const string MikroTikSettings = "/ddns/mikrotik-settings";
        public const string RaspberryPiSettings = "/ddns/raspberry-pi-settings";

        public const string MyWallet = "/wallet/my";
        public const string WalletTransactions = "/wallet/transactions";

        public const string Plans = "subscription/plans";
        public const string MySubscriptions = "subscription/my";
        public const string AllSubscriptions = "subscription/all";

        public const string AllTickets = "/tickets/all";
        public const string MyTickets = "/tickets/my";
        public const string TicketMessages = "/tickets/messages";

        public const string MyAccount = "/accounts/my";
        public const string Login = "/accounts/login";
        public const string Logout = "/accounts/logout";
        public const string RegisterAccount = "/accounts/register";
        public const string AllAcounts = "/accounts/all";
        public const string ForgetPassword = "/accounts/forget";
        public const string ResetPassword = "/accounts/reset";
        public const string ConfirmEmail = "/accounts/confirm-email";

        public const string AllPages = "/pages/all";
        public const string Page = "/pages/page";
        public const string Blog = "/blog";
        public const string AllBlogs = "/blog/all";
        public const string Wiki = "wiki";

        public const string CallBackPayment = "payment/callback";
        public const string Payments = "/payment/payments";
        public const string AllPayments = "/payment/all-payments";

        public const string TodayChangesDDNS = "/admin/today-changes-ddns";

        public const string Monitoring = "/monitoring/summary";

        public const string ReverseProxy = "/reverse-proxy/config";
        public const string SetupAndInstallation = "/reverse-proxy/setup-and-installation";
        public const string AuthToken = "/reverse-proxy/auth-token";
        public const string ActiveTunnels = "/reverse-proxy/active-tunnels";
        public const string TcpReverseProxy = "/reverse-proxy/tcp";
        public const string UdpReverseProxy = "/reverse-proxy/udp";
        public const string HttpReverseProxy = "/reverse-proxy/http";
    }
}
