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
        /// Gets the dictionary of cached user profiles keyed by username.
        /// </summary>
        public IDictionary<string, UserProfile> UserProfiles = new Dictionary<string, UserProfile>();

        /// <summary>
        /// Retrieves a user's public profile from Discogs.
        /// </summary>
        /// <param name="userName">The Discogs username.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The user's profile, or null if not found.</returns>
        /// <exception cref="ArgumentException">The API response could not be parsed into a profile.</exception>
        public async Task<UserProfile?> GetProfileAsync(string userName, CancellationToken ct = default)
        {
            if (this.UserProfiles.TryGetValue(userName, out UserProfile? existing) && existing != null)
            {
                return existing;
            }

            try
            {
                using HttpResponseMessage responseMessage = await this.DiscogsClient.DoGetRequestAsync($"/users/{userName}", ct);
                if (await this.JsonSerializationService.DeserializeContentAsync<UserProfile>(responseMessage, ct) is UserProfile userProfile)
                {
                    this.UserProfiles.Add(userName, userProfile);
                    return userProfile;
                }

                throw new ArgumentException("User profile cannot be parsed.");
            }
            catch (Exception exception)
            {
                this.DiscogsClient.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}
