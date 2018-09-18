using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudflareDnsUpdater
{
    public class PublicIP
    {
        private const string IpLocatorUrl = "https://api.ipify.org?format=text";
        private static Lazy<HttpClient> client = new Lazy<HttpClient>(() => { return new HttpClient() { BaseAddress = new Uri(IpLocatorUrl) }; });

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicIP"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private PublicIP(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; }

        /// <summary>
        /// Whats the is my public ip.
        /// </summary>
        /// <returns>public IP of this host</returns>
        public static async Task<PublicIP> WhatIsMyPublicIP()
        {
            var response = await client.Value.GetAsync(string.Empty);
            return new PublicIP(await response.Content.ReadAsStringAsync());
        }
    }
}
