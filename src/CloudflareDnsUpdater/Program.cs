using System;

namespace CloudflareDnsUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            new NotificationAreaUI().Launch();
            new CloudflarePublicIpUpdater().Start();
            while (Console.ReadLine() != "q") ;
        }
    }
}
