using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a Discogs user collection field.
    /// </summary>
    public class UserCollectionField
    {
        /// <summary>
        /// Gets or sets the user collection field identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the user collection field name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the user collection field position.
        /// </summary>
        [JsonPropertyName("position")]
        public int? Position { get; set; }

        /// <summary>
        /// Gets or sets whether this field is publicly visible or not.
        /// </summary>
        [JsonPropertyName("public")]
        public bool? IsPubliclyVisible { get; set; }

        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the number of lines for a textarea field.
        /// </summary>
        [JsonPropertyName("lines")]
        public int? Lines { get; set; }

        /// <summary>
        /// Gets or sets a collection of options.
        /// </summary>
        [JsonPropertyName("options")]
        public List<string> Options { get; set; } = [];
    }
}