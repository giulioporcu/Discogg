using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents artists and community data for a submission or contribution.
    /// </summary>
    public class Contribution
    {
        /// <summary>
        /// Gets or sets the artists associated with the contribution.
        /// </summary>
        [JsonPropertyName("artists")]
        public Artist[]? Artists { get; set; }

        /// <summary>
        /// Gets or sets the community metadata for the contribution.
        /// </summary>
        [JsonPropertyName("community")]
        public Community? Community { get; set; }
    }
}
