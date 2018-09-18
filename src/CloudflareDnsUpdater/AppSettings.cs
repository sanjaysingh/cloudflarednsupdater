using System.Configuration;

namespace CloudflareDnsUpdater
{
    public static class AppSettings
    {
        public static readonly string ZoneName = ConfigurationManager.AppSettings[nameof(ZoneName)];
        public static readonly string ApiKey = ConfigurationManager.AppSettings[nameof(ApiKey)];
        public static readonly string AuthEmail = ConfigurationManager.AppSettings[nameof(AuthEmail)];
        public static readonly string PublicIpUpdaterDnsName = ConfigurationManager.AppSettings[nameof(PublicIpUpdaterDnsName)];
        public static readonly int PublicIpUpdaterIntervalMs = int.Parse(ConfigurationManager.AppSettings[nameof(PublicIpUpdaterIntervalMs)]);
    }
}
