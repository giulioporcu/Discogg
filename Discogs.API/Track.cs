using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a track in a release, including position, title, duration,
    /// and any additional credited artists.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Gets or sets the track position as displayed on the release.
        /// </summary>
        [JsonPropertyName("position")]
        public string? Position { get; set; }

        /// <summary>
        /// Gets or sets the track type (e.g., track, index, heading).
        /// </summary>
        [JsonPropertyName("type_")]
        public string? TrackType { get; set; }

        /// <summary>
        /// Gets or sets the track title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets additional artists credited specifically for this track.
        /// </summary>
        [JsonPropertyName("extraartists")]
        public Artist[] ExtraArtists { get; set; } = [];

        /// <summary>
        /// Gets or sets the track duration as a string (e.g., "3:45").
        /// </summary>
        [JsonPropertyName("duration")]
        public string? Duration { get; set; }
    }
}
