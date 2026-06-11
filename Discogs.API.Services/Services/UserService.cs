using Discogs.API.Core;

namespace Discogs.API.Framework.Services
{
    /// <summary>
    /// Provides methods for retrieving Discogs user profiles.
    /// </summary>
    public class UserService(DiscogsClient discogsClient, JsonSerializationService jsonSerializationService)
    {
        /// <summary>
        /// Gets the Discogs client for API communication.
        /// </summary>
        private DiscogsClient DiscogsClient { get; } = discogsClient;

        /// <summary>
        /// Gets the JSON serialization service.
        /// </summary>
        private JsonSerializationService JsonSerializationService { get; } = jsonSerializationService;

        /// <summary>
        /// Retrieves a user's public profile from Discogs.
        /// </summary>
        /// <param name="userName">The Discogs username.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The user's profile, or null if not found.</returns>
        public async Task<UserProfile?> GetProfileAsync(string userName, CancellationToken ct = default)
        {
            try
            {
                using HttpResponseMessage responseMessage = await this.DiscogsClient.DoGetRequestAsync($"/users/{userName}", ct);
                return await this.JsonSerializationService.DeserializeContentAsync<UserProfile>(responseMessage, ct);
            }
            catch (Exception exception)
            {
                this.DiscogsClient.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}
