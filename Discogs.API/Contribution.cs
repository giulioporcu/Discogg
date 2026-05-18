using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    public class Contribution
    {
        [JsonPropertyName("artists")]
        public Artist[]? Artists { get; set; }

        [JsonPropertyName("community")]
        public Community? Community { get; set; }
    }
}
