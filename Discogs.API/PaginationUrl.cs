using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents pagination navigation URLs.
    /// </summary>
    public class PaginationUrl
    {
        /// <summary>
        /// Gets or sets the URL to the first page.
        /// </summary>
        [JsonPropertyName("first")]
        public string? First { get; set; }

        /// <summary>
        /// Gets or sets the URL to the previous page.
        /// </summary>
        [JsonPropertyName("prev")]
        public string? Prev { get; set; }

        /// <summary>
        /// Gets or sets the URL to the next page.
        /// </summary>
        [JsonPropertyName("next")]
        public string? Next { get; set; }

        /// <summary>
        /// Gets or sets the URL to the last page.
        /// </summary>
        [JsonPropertyName("last")]
        public string? Last { get; set; }
    }
}
