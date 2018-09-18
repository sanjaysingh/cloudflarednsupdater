using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CloudflareDnsUpdater
{
    public class CloudflarePublicIpUpdater
    {
        private Timer timer;
        private string lastKnownDnsIp;

        public void Start()
        {
            timer = new Timer(AppSettings.PublicIpUpdaterIntervalMs);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            this.UpdateDnsIfRequired();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Dispose();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateDnsIfRequired();
        }

        private void UpdateDnsIfRequired()
        {
            try
            {
                var currentPublicIp = PublicIP.WhatIsMyPublicIP().Result;

                if (lastKnownDnsIp !=  null && lastKnownDnsIp != currentPublicIp.Value)
                {
                    Log($"Not updated since Dns mapping is still correct. {AppSettings.PublicIpUpdaterDnsName} = {lastKnownDnsIp}");
                    return;
                }

                var cloudflareClient = new CloudflareClient(AppSettings.ApiKey, AppSettings.AuthEmail);
                var zone = cloudflareClient.GetZones().Result.FirstOrDefault(x => x.ZoneName.Equals(AppSettings.ZoneName, StringComparison.OrdinalIgnoreCase));
                if (zone == null)
                {
                    Log($"Given zone name '{AppSettings.ZoneName}' was not found. ");
                    return;
                }

                var currentDnsRecord = cloudflareClient.GetDnsRecords(zone.ZoneId).Result.FirstOrDefault(x => x.Name.Equals(AppSettings.PublicIpUpdaterDnsName, StringComparison.OrdinalIgnoreCase));
                if (currentDnsRecord == null)
                {
                    Log($"Given DNS name '{AppSettings.PublicIpUpdaterDnsName}' was not found. ");
                    return;
                }

                if(currentDnsRecord.Content != currentPublicIp.Value)
                {
                    var newDnsRecord = new DnsRecord(currentDnsRecord.Id, currentDnsRecord.Type, currentDnsRecord.Name, currentPublicIp.Value);
                    cloudflareClient.UpdateDnsRecord(zone.ZoneId, newDnsRecord).Wait();
                    lastKnownDnsIp = currentPublicIp.Value;
                    Log($"Updated dns mapping. {currentDnsRecord.Name} = {lastKnownDnsIp}");
                }
                else
                {
                    lastKnownDnsIp = currentPublicIp.Value;
                    Log($"Not updated since Dns mapping is still correct. {AppSettings.PublicIpUpdaterDnsName} = {lastKnownDnsIp}");
                }
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString()}\t {message}");
        }
    }
}
