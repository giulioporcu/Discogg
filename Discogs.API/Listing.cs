using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    /// <summary>
    /// Represents a marketplace listing entry.
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// Gets or sets the listing identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL for this listing.
        /// </summary>
        [JsonPropertyName("resource_url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the Discogs URI for this listing.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets or sets the release associated with this listing.
        /// </summary>
        [JsonPropertyName("release")]
        public Release? Release { get; set; }

        /// <summary>
        /// Gets or sets the seller information.
        /// </summary>
        [JsonPropertyName("seller")]
        public UserProfile? Seller { get; set; }

        /// <summary>
        /// Gets or sets the listing price.
        /// </summary>
        [JsonPropertyName("price")]
        public ListingPrice? Price { get; set; }

        /// <summary>
        /// Gets or sets the item condition (e.g., Mint, Near Mint, Very Good Plus).
        /// </summary>
        [JsonPropertyName("condition")]
        public string? Condition { get; set; }

        /// <summary>
        /// Gets or sets the sleeve condition.
        /// </summary>
        [JsonPropertyName("sleeve_condition")]
        public string? SleeveCondition { get; set; }

        /// <summary>
        /// Gets or sets the listing status (e.g., For Sale, Sold, Draft).
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the date the listing was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the listing description/notes.
        /// </summary>
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets whether the listing is blocked from sale.
        /// </summary>
        [JsonPropertyName("blocked_from_sale")]
        public bool? BlockedFromSale { get; set; }

        /// <summary>
        /// Gets or sets the weight of the item.
        /// </summary>
        [JsonPropertyName("weight")]
        public int? Weight { get; set; }

        /// <summary>
        /// Gets or sets the format quantity.
        /// </summary>
        [JsonPropertyName("format_quantity")]
        public int? FormatQuantity { get; set; }

        /// <summary>
        /// Gets or sets the discogs listing URL for external reference.
        /// </summary>
        [JsonPropertyName("discogs_listing_url")]
        public string? DiscogsListingUrl { get; set; }

        /// <summary>
        /// Gets or sets the external identifier for the listing.
        /// </summary>
        [JsonPropertyName("external_id")]
        public string? ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the location country.
        /// </summary>
        [JsonPropertyName("location_country")]
        public string? LocationCountry { get; set; }

        /// <summary>
        /// Gets or sets the sale note to buyer.
        /// </summary>
        [JsonPropertyName("sale_note_to_buyer")]
        public string? SaleNoteToBuyer { get; set; }

        /// <summary>
        /// Gets or sets the offer status.
        /// </summary>
        [JsonPropertyName("allow_offers")]
        public bool? AllowOffers { get; set; }
    }

    /// <summary>
    /// Represents pricing information for a marketplace listing.
    /// </summary>
    public class ListingPrice
    {
        /// <summary>
        /// Gets or sets the currency code (e.g., USD, EUR, GBP).
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        /// Gets or sets the listing price value.
        /// </summary>
        [JsonPropertyName("value")]
        public float? Value { get; set; }

        /// <summary>
        /// Gets or sets the price without currency symbol.
        /// </summary>
        [JsonPropertyName("price")]
        public float? Price { get; set; }

        /// <summary>
        /// Gets or sets the suggested price from Discogs.
        /// </summary>
        [JsonPropertyName("suggested_price")]
        public float? SuggestedPrice { get; set; }

        /// <summary>
        /// Gets or sets the converted price value.
        /// </summary>
        [JsonPropertyName("converted_price")]
        public float? ConvertedPrice { get; set; }
    }

    /// <summary>
    /// Represents a marketplace order.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL for this order.
        /// </summary>
        [JsonPropertyName("resource_url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the Discogs URI for this order.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets or sets the order status (e.g., New, Pending, Shipped, Completed, Cancelled).
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the buyer information.
        /// </summary>
        [JsonPropertyName("buyer")]
        public UserProfile? Buyer { get; set; }

        /// <summary>
        /// Gets or sets the seller information.
        /// </summary>
        [JsonPropertyName("seller")]
        public UserProfile? Seller { get; set; }

        /// <summary>
        /// Gets or sets the buyer address identifier.
        /// </summary>
        [JsonPropertyName("buyer_address_id")]
        public int? BuyerAddressId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        [JsonPropertyName("shipping_address")]
        public Address? ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the additional address lines.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the buyer username.
        /// </summary>
        [JsonPropertyName("buyer_username")]
        public string? BuyerUsername { get; set; }

        /// <summary>
        /// Gets or sets the seller username.
        /// </summary>
        [JsonPropertyName("seller_username")]
        public string? SellerUsername { get; set; }

        /// <summary>
        /// Gets or sets the order items.
        /// </summary>
        [JsonPropertyName("items")]
        public OrderItem[]? Items { get; set; }

        /// <summary>
        /// Gets or sets the total price.
        /// </summary>
        [JsonPropertyName("total")]
        public string? Total { get; set; }

        /// <summary>
        /// Gets or sets the combined total with shipping.
        /// </summary>
        [JsonPropertyName("combined_total")]
        public string? CombinedTotal { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        [JsonPropertyName("last_activity")]
        public string? LastActivity { get; set; }

        /// <summary>
        /// Gets or sets the shipping method.
        /// </summary>
        [JsonPropertyName("shipping_method")]
        public string? ShippingMethod { get; set; }

        /// <summary>
        /// Gets or sets the tracking number.
        /// </summary>
        [JsonPropertyName("tracking_number")]
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets the tracking URL.
        /// </summary>
        [JsonPropertyName("tracking_url")]
        public string? TrackingUrl { get; set; }

        /// <summary>
        /// Gets or sets the USPS tracking URL.
        /// </summary>
        [JsonPropertyName("usps_tracking_url")]
        public string? UspsTrackingUrl { get; set; }

        /// <summary>
        /// Gets or sets the estimated shipping date.
        /// </summary>
        [JsonPropertyName("ship_date")]
        public string? ShipDate { get; set; }

        /// <summary>
        /// Gets or sets the order fee.
        /// </summary>
        [JsonPropertyName("fee")]
        public float? Fee { get; set; }

        /// <summary>
        /// Gets or sets the fee currency.
        /// </summary>
        [JsonPropertyName("fee_currency")]
        public string? FeeCurrency { get; set; }

        /// <summary>
        /// Gets or sets whether the order is a combined order.
        /// </summary>
        [JsonPropertyName("is_combined_order")]
        public bool? IsCombinedOrder { get; set; }

        /// <summary>
        /// Gets or sets the order messages.
        /// </summary>
        [JsonPropertyName("messages")]
        public OrderMessage[]? Messages { get; set; }

        /// <summary>
        /// Gets or sets the message count.
        /// </summary>
        [JsonPropertyName("message_count")]
        public int? MessageCount { get; set; }

        /// <summary>
        /// Gets or sets the shipping available countries.
        /// </summary>
        [JsonPropertyName("shipping_available_countries")]
        public string[]? ShippingAvailableCountries { get; set; }

        /// <summary>
        /// Gets or sets the items total.
        /// </summary>
        [JsonPropertyName("items_total")]
        public float? ItemsTotal { get; set; }

        /// <summary>
        /// Gets or sets the automatically calculated total.
        /// </summary>
        [JsonPropertyName("automated_total")]
        public float? AutomatedTotal { get; set; }

        /// <summary>
        /// Gets or sets the release ID.
        /// </summary>
        [JsonPropertyName("release_id")]
        public int? ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the order notes.
        /// </summary>
        [JsonPropertyName("notes")]
        public string[]? Notes { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        [JsonPropertyName("invoice_number")]
        public string? InvoiceNumber { get; set; }
    }

    /// <summary>
    /// Represents an item within an order.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the listing identifier.
        /// </summary>
        [JsonPropertyName("listing_id")]
        public int? ListingId { get; set; }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        [JsonPropertyName("price")]
        public float? Price { get; set; }

        /// <summary>
        /// Gets or sets the item condition.
        /// </summary>
        [JsonPropertyName("condition")]
        public string? Condition { get; set; }

        /// <summary>
        /// Gets or sets the sleeve condition.
        /// </summary>
        [JsonPropertyName("sleeve_condition")]
        public string? SleeveCondition { get; set; }

        /// <summary>
        /// Gets or sets the item status.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the release associated with this item.
        /// </summary>
        [JsonPropertyName("release")]
        public Release? Release { get; set; }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }
    }

    /// <summary>
    /// Represents a shipping or billing address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the address name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the first line of the address.
        /// </summary>
        [JsonPropertyName("address_1")]
        public string? Address1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the address.
        /// </summary>
        [JsonPropertyName("address_2")]
        public string? Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the state or region.
        /// </summary>
        [JsonPropertyName("state")]
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }

    /// <summary>
    /// Represents a message within an order.
    /// </summary>
    public class OrderMessage
    {
        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the sender information.
        /// </summary>
        [JsonPropertyName("from")]
        public UserProfile? From { get; set; }

        /// <summary>
        /// Gets or sets the sender username.
        /// </summary>
        [JsonPropertyName("from_username")]
        public string? FromUsername { get; set; }

        /// <summary>
        /// Gets or sets the recipient username.
        /// </summary>
        [JsonPropertyName("to_username")]
        public string? ToUsername { get; set; }

        /// <summary>
        /// Gets or sets the message subject.
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        [JsonPropertyName("body")]
        public string? Body { get; set; }

        /// <summary>
        /// Gets or sets the message creation date.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the message status.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the source of the message.
        /// </summary>
        [JsonPropertyName("source")]
        public string? Source { get; set; }

        /// <summary>
        /// Gets or sets the message URL.
        /// </summary>
        [JsonPropertyName("message_url")]
        public string? MessageUrl { get; set; }

        /// <summary>
        /// Gets or sets the attachment description.
        /// </summary>
        [JsonPropertyName("attachment_description")]
        public string? AttachmentDescription { get; set; }

        /// <summary>
        /// Gets or sets the attachment filename.
        /// </summary>
        [JsonPropertyName("attachment_filename")]
        public string? AttachmentFilename { get; set; }
    }

    /// <summary>
    /// Represents a user-created list.
    /// </summary>
    public class UserList
    {
        /// <summary>
        /// Gets or sets the list identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL for this list.
        /// </summary>
        [JsonPropertyName("resource_url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the Discogs URI for this list.
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Gets or sets the list name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the list description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the list visibility (e.g., Public, Private).
        /// </summary>
        [JsonPropertyName("visibility")]
        public string? Visibility { get; set; }

        /// <summary>
        /// Gets or sets the owner information.
        /// </summary>
        [JsonPropertyName("owner")]
        public UserProfile? Owner { get; set; }

        /// <summary>
        /// Gets or sets the date the list was created.
        /// </summary>
        [JsonPropertyName("date_created")]
        public string? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date the list was last modified.
        /// </summary>
        [JsonPropertyName("date_modified")]
        public string? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the number of items in the list.
        /// </summary>
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        /// <summary>
        /// Gets or sets the list thumbnail image.
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the custom list.
        /// </summary>
        [JsonPropertyName("custom")]
        public bool? Custom { get; set; }

        /// <summary>
        /// Gets or sets the list items.
        /// </summary>
        [JsonPropertyName("items")]
        public UserListItem[]? Items { get; set; }
    }

    /// <summary>
    /// Represents an item within a user list.
    /// </summary>
    public class UserListItem
    {
        /// <summary>
        /// Gets or sets the display rank.
        /// </summary>
        [JsonPropertyName("display_rank")]
        public string? DisplayRank { get; set; }

        /// <summary>
        /// Gets or sets the resource type (e.g., release, artist).
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the API resource URL.
        /// </summary>
        [JsonPropertyName("resource_url")]
        public string? ResourceUrl { get; set; }

        /// <summary>
        /// Gets or sets the release associated with this item.
        /// </summary>
        [JsonPropertyName("release")]
        public Release? Release { get; set; }

        /// <summary>
        /// Gets or sets the artist associated with this item.
        /// </summary>
        [JsonPropertyName("artist")]
        public Artist? Artist { get; set; }

        /// <summary>
        /// Gets or sets the label associated with this item.
        /// </summary>
        [JsonPropertyName("label")]
        public Label? Label { get; set; }

        /// <summary>
        /// Gets or sets the catalog number.
        /// </summary>
        [JsonPropertyName("catalog_number")]
        public string? CatalogNumber { get; set; }

        /// <summary>
        /// Gets or sets the barcode.
        /// </summary>
        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        /// <summary>
        /// Gets or sets the item format.
        /// </summary>
        [JsonPropertyName("format")]
        public string? Format { get; set; }

        /// <summary>
        /// Gets or sets the release year.
        /// </summary>
        [JsonPropertyName("year")]
        public string? Year { get; set; }

        /// <summary>
        /// Gets or sets the item title.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the thumb image URL.
        /// </summary>
        [JsonPropertyName("thumb")]
        public string? Thumb { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }
}
