using System;

namespace CloudflareDnsUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrWhiteSpace(AppSettings.ApiKey))
            {
                Console.WriteLine("Apikey is not specified. Check config file at exe loation.");
            }

            if (string.IsNullOrWhiteSpace(AppSettings.AuthEmail))
            {
                Console.WriteLine("AuthEmail is not specified. Check config file at exe loation.");
            }

            if (string.IsNullOrWhiteSpace(AppSettings.PublicIpUpdaterDnsName))
            {
                Console.WriteLine("Dns name is not specified. Check config file at exe loation.");
            }

            if (string.IsNullOrWhiteSpace(AppSettings.ZoneName))
            {
                Console.WriteLine("Zone name is not specified. Check config file at exe loation.");
            }

            new NotificationAreaUI().Launch();
            new CloudflarePublicIpUpdater().Start();
            while (Console.ReadLine() != "q") ;
        }
    }
}
