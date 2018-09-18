using System;
using System.Net.Http;

namespace CloudflareDnsUpdater
{
    /// <summary>
    /// Cloud flare zone
    /// </summary>
    public class CloudflareZone
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloudflareZone"/> class.
        /// </summary>
        /// <param name="zoneId">The zone identifier.</param>
        /// <param name="zoneName">Name of the zone.</param>
        public CloudflareZone(string zoneId, string zoneName)
        {
            this.ZoneId = zoneId;
            this.ZoneName = zoneName;
        }

        /// <summary>
        /// Gets the zone identifier.
        /// </summary>
        /// <value>
        /// The zone identifier.
        /// </value>
        public string ZoneId { get; }

        /// <summary>
        /// Gets the name of the zone.
        /// </summary>
        /// <value>
        /// The name of the zone.
        /// </value>
        public string ZoneName { get; }
    }
}
