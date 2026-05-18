using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a release format, including name, quantity, descriptions, and optional text.
    /// </summary>
    public class Format
    {
        /// <summary>
        /// Gets or sets the format name (e.g., Vinyl, CD, File).
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the quantity of items in this format.
        /// </summary>
        [JsonPropertyName("qty")]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets descriptive attributes of the format (e.g., Album, Stereo).
        /// </summary>
        [JsonPropertyName("descriptions")]
        public string[] Descriptions { get; set; } = [];

        /// <summary>
        /// Gets or sets additional free‑text information about the format.
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
