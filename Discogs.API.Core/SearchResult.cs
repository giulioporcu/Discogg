using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a search result from the Discogs database search endpoint.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the resource type (e.g., release, master, artist, label).
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL for this resource.
        /// </summary>
        [JsonPropertyName("resource_url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the Discogs URI for this resource.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets or sets the resource title (varies by type).
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail image URL.
        /// </summary>
        [JsonPropertyName("thumb")]
        public string? Thumb { get; set; }

        /// <summary>
        /// Gets or sets the cover image URL.
        /// </summary>
        [JsonPropertyName("cover_image")]
        public string? CoverImage { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL (for labels/artists).
        /// </summary>
        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the primary artist name.
        /// </summary>
        [JsonPropertyName("artist")]
        public string? Artist { get; set; }

        /// <summary>
        /// Gets or sets the artist credit display string.
        /// </summary>
        [JsonPropertyName("artist_credit")]
        public string? ArtistCredit { get; set; }

        /// <summary>
        /// Gets or sets the featured artist name.
        /// </summary>
        [JsonPropertyName("featured_artist")]
        public string? FeaturedArtist { get; set; }

        /// <summary>
        /// Gets or sets the featured artist ID.
        /// </summary>
        [JsonPropertyName("featured_artist_id")]
        public int? FeaturedArtistId { get; set; }

        /// <summary>
        /// Gets or sets the release title.
        /// </summary>
        [JsonPropertyName("release_title")]
        public string? ReleaseTitle { get; set; }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        /// <summary>
        /// Gets or sets the label array (for full responses).
        /// </summary>
        [JsonPropertyName("labels")]
        public string[]? Labels { get; set; }

        /// <summary>
        /// Gets or sets the catalog number.
        /// </summary>
        [JsonPropertyName("catno")]
        public string? CatalogNumber { get; set; }

        /// <summary>
        /// Gets or sets the barcode.
        /// </summary>
        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        /// <summary>
        /// Gets or sets the country of release.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the year of release.
        /// </summary>
        [JsonPropertyName("year")]
        public string? Year { get; set; }

        /// <summary>
        /// Gets or sets the release format.
        /// </summary>
        [JsonPropertyName("format")]
        public string? Format { get; set; }

        /// <summary>
        /// Gets or sets the formats array (for full responses).
        /// </summary>
        [JsonPropertyName("formats")]
        public string[]? Formats { get; set; }

        /// <summary>
        /// Gets or sets the genres array.
        /// </summary>
        [JsonPropertyName("genre")]
        public string[]? Genres { get; set; }

        /// <summary>
        /// Gets or sets the styles array.
        /// </summary>
        [JsonPropertyName("style")]
        public string[]? Styles { get; set; }

        /// <summary>
        /// Gets or sets the master release identifier.
        /// </summary>
        [JsonPropertyName("master_id")]
        public int? MasterId { get; set; }

        /// <summary>
        /// Gets or sets the master release URL.
        /// </summary>
        [JsonPropertyName("master_url")]
        public string? MasterUrl { get; set; }

        /// <summary>
        /// Gets or sets the community have count.
        /// </summary>
        [JsonPropertyName("community_have")]
        public int? CommunityHave { get; set; }

        /// <summary>
        /// Gets or sets the community want count.
        /// </summary>
        [JsonPropertyName("community_want")]
        public int? CommunityWant { get; set; }

        /// <summary>
        /// Gets or sets the submitter information.
        /// </summary>
        [JsonPropertyName("submitter")]
        public Submitter? Submitter { get; set; }

        /// <summary>
        /// Gets or sets the contributors array.
        /// </summary>
        [JsonPropertyName("contributors")]
        public Contributor[]? Contributors { get; set; }

        /// <summary>
        /// Gets or sets the number of copies for sale.
        /// </summary>
        [JsonPropertyName("num_for_sale")]
        public int? NumberForSale { get; set; }

        /// <summary>
        /// Gets or sets the lowest price.
        /// </summary>
        [JsonPropertyName("lowest_price")]
        public float? LowestPrice { get; set; }

        /// <summary>
        /// Gets or sets the community rating average.
        /// </summary>
        [JsonPropertyName("community_rating")]
        public float? CommunityRating { get; set; }

        /// <summary>
        /// Gets or sets the average community rating value.
        /// </summary>
        [JsonPropertyName("rating")]
        public float? Rating { get; set; }

        /// <summary>
        /// Gets or sets the vote count.
        /// </summary>
        [JsonPropertyName("vote_count")]
        public int? VoteCount { get; set; }

        /// <summary>
        /// Gets or sets the ANV (Artist Name Variation).
        /// </summary>
        [JsonPropertyName("anv")]
        public string? Anv { get; set; }

        /// <summary>
        /// Gets or sets the user collection information.
        /// </summary>
        [JsonPropertyName("user_collection")]
        public object[]? UserCollection { get; set; }

        /// <summary>
        /// Gets or sets the track information.
        /// </summary>
        [JsonPropertyName("track")]
        public string? Track { get; set; }

        /// <summary>
        /// Gets or sets the alias information.
        /// </summary>
        [JsonPropertyName("alias")]
        public string? Alias { get; set; }

        /// <summary>
        /// Gets or sets the real name of the artist.
        /// </summary>
        [JsonPropertyName("realname")]
        public string? RealName { get; set; }

        /// <summary>
        /// Gets or sets the profile information.
        /// </summary>
        [JsonPropertyName("profile")]
        public string? Profile { get; set; }

        /// <summary>
        /// Gets or sets the parent label.
        /// </summary>
        [JsonPropertyName("parent_label")]
        public string? ParentLabel { get; set; }

        /// <summary>
        /// Gets or sets the data quality status.
        /// </summary>
        [JsonPropertyName("data_quality")]
        public string? DataQuality { get; set; }

        /// <summary>
        /// Gets or sets the release status.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets external IDs.
        /// </summary>
        [JsonPropertyName("extreme_id")]
        public string? ExtremeId { get; set; }
    }
}
