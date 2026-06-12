using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a label associated with a release or as a standalone entity.
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the catalog number assigned by the label.
        /// </summary>
        [JsonPropertyName("catno")]
        public string? CatalogNumber { get; set; }

        /// <summary>
        /// Gets or sets the numeric entity type identifier.
        /// </summary>
        [JsonPropertyName("entity_type")]
        public string? EntityType { get; set; }

        /// <summary>
        /// Gets or sets the descriptive name of the entity type.
        /// </summary>
        [JsonPropertyName("entity_type_name")]
        public string? EntityTypeName { get; set; }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL for the label.
        /// </summary>
        [JsonPropertyName("resource_url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the label's thumbnail image.
        /// </summary>
        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the label's profile text.
        /// </summary>
        [JsonPropertyName("profile")]
        public string? Profile { get; set; }

        /// <summary>
        /// Gets or sets the Discogs URI for the label.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets or sets the parent label name.
        /// </summary>
        [JsonPropertyName("parent_label")]
        public string? ParentLabel { get; set; }

        /// <summary>
        /// Gets or sets the parent label data.
        /// </summary>
        [JsonPropertyName("parentEntity")]
        public Label? ParentEntity { get; set; }

        /// <summary>
        /// Gets or sets the sub-labels.
        /// </summary>
        [JsonPropertyName("sublabels")]
        public string[]? SubLabels { get; set; }

        /// <summary>
        /// Gets or sets the URLs associated with the label.
        /// </summary>
        [JsonPropertyName("urls")]
        public string[]? Urls { get; set; }

        /// <summary>
        /// Gets or sets the URL for the label's images.
        /// </summary>
        [JsonPropertyName("images_url")]
        public string? ImagesUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL for the label's releases.
        /// </summary>
        [JsonPropertyName("releases_url")]
        public string? ReleasesUrl { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL for the label's releases.
        /// </summary>
        [JsonPropertyName("releases_resource_url")]
        public string? ReleasesResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the name variations for the label.
        /// </summary>
        [JsonPropertyName("namevariations")]
        public string[]? NameVariations { get; set; }

        /// <summary>
        /// Gets or sets the contact information for the label.
        /// </summary>
        [JsonPropertyName("contactinfo")]
        public string? ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the data quality status.
        /// </summary>
        [JsonPropertyName("data_quality")]
        public string? DataQuality { get; set; }

        /// <summary>
        /// Gets or sets the number of releases.
        /// </summary>
        [JsonPropertyName("releases_count")]
        public int? ReleasesCount { get; set; }

        /// <summary>
        /// Gets or sets the images array.
        /// </summary>
        [JsonPropertyName("images")]
        public Image[]? Images { get; set; }

        /// <summary>
        /// Gets or sets the country of the label.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the date the label was founded.
        /// </summary>
        [JsonPropertyName("founded")]
        public string? Founded { get; set; }

        /// <summary>
        /// Gets or sets the date the label was discontinued.
        /// </summary>
        [JsonPropertyName("discontinued")]
        public string? Discontinued { get; set; }

        /// <summary>
        /// Gets or sets the label code (e.g., LC).
        /// </summary>
        [JsonPropertyName("labelcode")]
        public string? LabelCode { get; set; }

        /// <summary>
        /// Gets or sets the URI to the user's wantlist for this label.
        /// </summary>
        [JsonPropertyName("uri_wantlist")]
        public string? UriWantlist { get; set; }

        /// <summary>
        /// Gets or sets the URI to the user's collection for this label.
        /// </summary>
        [JsonPropertyName("uri_collection")]
        public string? UriCollection { get; set; }
    }
}
