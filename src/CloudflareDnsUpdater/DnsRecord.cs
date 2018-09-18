using Newtonsoft.Json;

namespace CloudflareDnsUpdater
{
    /// <summary>
    /// Dns record of a zone
    /// </summary>
    public class DnsRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DnsRecord"/> class.
        /// </summary>
        /// <param name="recordType">Type of the record.</param>
        /// <param name="name">The name.</param>
        /// <param name="content">The content.</param>
        public DnsRecord(string id, string recordType, string name, string content)
        {
            this.Id = id;
            this.Type = recordType;
            this.Name = name;
            this.Content = content;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the type of the record.
        /// </summary>
        /// <value>
        /// The type of the record.
        /// </value>
        public string Type { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; }
    }
}
