using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a paginated response from the Discogs.API.Core.
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Gets or sets the page number (1-indexed).
        /// </summary>
        [JsonPropertyName("page")]
        public int? Page { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        [JsonPropertyName("per_page")]
        public int? PerPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        [JsonPropertyName("pages")]
        public int? Pages { get; set; }

        /// <summary>
        /// Gets or sets the total number of items.
        /// </summary>
        [JsonPropertyName("items")]
        public int? Items { get; set; }

        /// <summary>
        /// Gets or sets the URLs for pagination navigation.
        /// </summary>
        [JsonPropertyName("urls")]
        public PaginationUrl[]? Urls { get; set; }
    }
}
