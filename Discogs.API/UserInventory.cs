using System.Text.Json.Serialization;

namespace Discogs.API.Core
{
    public class UserInventory
    {
        [JsonPropertyName("pagination")]
        public Pagination? Pagination { get; set; }

        [JsonPropertyName("listings")]
        public Listing[]? Listings { get; set; }
    }
}
