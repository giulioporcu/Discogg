using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a Discogs user account with profile details, activity statistics,
    /// marketplace information, and related resource URLs.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL for this user.
        /// </summary>
        [JsonPropertyName("resource_url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the Discogs URI for this user.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the user's homepage URL.
        /// </summary>
        [JsonPropertyName("home_page")]
        public string? HomePage { get; set; }

        /// <summary>
        /// Gets or sets the user's location.
        /// </summary>
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the user's profile text.
        /// </summary>
        [JsonPropertyName("profile")]
        public string? Profile { get; set; }

        /// <summary>
        /// Gets or sets the registration date string.
        /// </summary>
        [JsonPropertyName("registered")]
        public string? RegisteredDate { get; set; }

        /// <summary>
        /// Gets or sets the user's rank.
        /// </summary>
        [JsonPropertyName("rank")]
        public double? Rank { get; set; }

        /// <summary>
        /// Gets or sets the number of pending submissions.
        /// </summary>
        [JsonPropertyName("num_pending")]
        public int? NumberOfSubmissionsPending { get; set; }

        [JsonPropertyName("num_wantlist")]
        public int? NumberOfWantListItems { get; set; }

        /// <summary>
        /// Gets or sets the number of items the user has for sale.
        /// </summary>
        [JsonPropertyName("num_for_sale")]
        public int? NumberOfItemsForSale { get; set; }

        /// <summary>
        /// Gets or sets the number of lists created by the user.
        /// </summary>
        [JsonPropertyName("num_lists")]
        public int? NumberOfLists { get; set; }

        /// <summary>
        /// Gets or sets the number of releases the user has contributed.
        /// </summary>
        [JsonPropertyName("releases_contributed")]
        public int? ReleasesContributed { get; set; }

        /// <summary>
        /// Gets or sets the number of releases the user has rated.
        /// </summary>
        [JsonPropertyName("releases_rated")]
        public int? ReleasesRated { get; set; }

        /// <summary>
        /// Gets or sets the user's average release rating.
        /// </summary>
        [JsonPropertyName("rating_avg")]
        public double? RatingAverage { get; set; }

        /// <summary>
        /// Gets or sets the URL to the user's inventory.
        /// </summary>
        [JsonPropertyName("inventory_url")]
        public string? InventoryUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to the user's collection folders.
        /// </summary>
        [JsonPropertyName("collection_folders_url")]
        public string? CollectionFoldersUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to the user's collection fields.
        /// </summary>
        [JsonPropertyName("collection_fields_url")]
        public string? CollectionFieldsUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to the user's wantlist.
        /// </summary>
        [JsonPropertyName("wantlist_url")]
        public string? WantListUrl { get; set; }

        /// <summary>
        /// Gets or sets the user's avatar image URL.
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the user's preferred currency abbreviation.
        /// </summary>
        [JsonPropertyName("curr_abbr")]
        public string? CurrencyAbbreviated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user account is activated.
        /// </summary>
        [JsonPropertyName("activated")]
        public bool? IsActivated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is suspended from the marketplace.
        /// </summary>
        [JsonPropertyName("marketplace_suspended")]
        public bool? MarketplaceSuspended { get; set; }

        /// <summary>
        /// Gets or sets the user's banner image URL.
        /// </summary>
        [JsonPropertyName("banner_url")]
        public string? BannerUrl { get; set; }

        /// <summary>
        /// Gets or sets the user's buyer rating.
        /// </summary>
        [JsonPropertyName("buyer_rating")]
        public double? BuyerRating { get; set; }

        /// <summary>
        /// Gets or sets the user's buyer rating in stars.
        /// </summary>
        [JsonPropertyName("buyer_rating_stars")]
        public double? BuyerRatingStars { get; set; }

        /// <summary>
        /// Gets or sets the number of buyer ratings received.
        /// </summary>
        [JsonPropertyName("buyer_num_ratings")]
        public int? BuyerNumRatings { get; set; }

        /// <summary>
        /// Gets or sets the user's seller rating.
        /// </summary>
        [JsonPropertyName("seller_rating")]
        public double? SellerRating { get; set; }

        /// <summary>
        /// Gets or sets the user's seller rating in stars.
        /// </summary>
        [JsonPropertyName("seller_rating_stars")]
        public double? SellerRatingStars { get; set; }

        /// <summary>
        /// Gets or sets the number of seller ratings received.
        /// </summary>
        [JsonPropertyName("seller_num_ratings")]
        public int? SellerNumRatings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is a Discogs staff member.
        /// </summary>
        [JsonPropertyName("is_staff")]
        public bool? IsStaff { get; set; }
    }
}