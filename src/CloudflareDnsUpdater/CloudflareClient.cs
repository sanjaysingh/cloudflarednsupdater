using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudflareDnsUpdater
{
    /// <summary>
    /// Cloud flare client
    /// </summary>
    public class CloudflareClient
    {
        private const string BaseAddress = "https://api.cloudflare.com/client/v4/";
        private readonly HttpClient client = new HttpClient() { BaseAddress = new Uri(BaseAddress) };

        /// <summary>
        /// Creates a new instace of cloud flare client
        /// </summary>
        /// <param name="apiKey">the api key for cloudflare access</param>
        /// <param name="authEmail">the login email associated with the cloudflare account</param>
        public CloudflareClient(string apiKey, string authEmail)
        {
            this.client.DefaultRequestHeaders.Add("X-AUTH-KEY", apiKey);
            this.client.DefaultRequestHeaders.Add("X-AUTH-EMAIL", authEmail);
        }

        /// <summary>
        /// Gets the zone.
        /// </summary>
        /// <returns>
        /// cloudflare zone or null if not found
        /// </returns>
        public async Task<IEnumerable<CloudflareZone>> GetZones()
        {
            var response = await (await client.GetAsync("zones")).Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(response);
            var zones = new List<CloudflareZone>();
            foreach (var result in jsonResponse.result)
            {
                zones.Add(new CloudflareZone(result.id.ToString(), result.name.ToString()));
            }

            return zones;
        }

        /// <summary>
        /// Gets the DNS records.
        /// </summary>
        /// <param name="zoneId">The zone identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<DnsRecord>> GetDnsRecords(string zoneId)
        {
            var response = await (await client.GetAsync($"zones/{zoneId}/dns_records")).Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(response);
            var dnsRecords = new List<DnsRecord>();
            foreach (var result in jsonResponse.result)
            {
                dnsRecords.Add(new DnsRecord(result.id.ToString(), result.type.ToString(), result.name.ToString(), result.content.ToString()));
            }

            return dnsRecords;
        }

        /// <summary>
        /// Updates the DNS record.
        /// </summary>
        /// <param name="zoneId">The zone identifier.</param>
        /// <param name="dnsRecord">The DNS record.</param>
        /// <returns></returns>
        public async Task UpdateDnsRecord(string zoneId, DnsRecord dnsRecord)
        {
            var data = JsonConvert.SerializeObject(new { type = dnsRecord.Type, name = dnsRecord.Name, content = dnsRecord.Content });
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync($"zones/{zoneId}/dns_records/{dnsRecord.Id}", content);
        }
    }
}
