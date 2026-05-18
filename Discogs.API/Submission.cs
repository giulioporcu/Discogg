using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    public class Submission
    {
        [JsonPropertyName("artists")]
        public Artist[]? Artists { get; set; }

        [JsonPropertyName("labels")]
        public Label[]? Labels { get; set; }

        [JsonPropertyName("releases")]
        public Release[]? Releases { get; set; }
    }
}
