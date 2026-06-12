using Discogs.API.Core;
using Discogs.API.Framework.Extensions;
using System.Text.Json;

namespace Discogs.API.Framework.Services
{
    /// <summary>
    /// Provides methods for retrieving Discogs user profiles.
    /// </summary>
    public class UserService(DiscogsClient discogsClient)
    {
        /// <summary>
        /// Gets the Discogs client for API communication.
        /// </summary>
        private DiscogsClient DiscogsClient { get; } = discogsClient;

        /// <summary>
        /// Gets the JSON serialization service.
        /// </summary>

        /// <summary>
        /// Gets the dictionary of cached user profiles keyed by username.
        /// </summary>
        private Dictionary<string, UserProfile> UserProfiles { get; } = [];

        /// <summary>
        /// Retrieves a user's public profile from Discogs.
        /// </summary>
        /// <param name="userName">The Discogs username.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The user's profile, or null if not found.</returns>
        /// <exception cref="ArgumentException">The API response could not be parsed into a profile.</exception>
        public async Task<UserProfile?> GetProfileAsync(string userName, CancellationToken ct = default)
        {
            if (this.UserProfiles.TryGetValue(userName, out UserProfile? existing))
            {
                return existing;
            }

            try
            {
                string path = $"/users/{userName}";
                using HttpResponseMessage response = await this.DiscogsClient.DoGetRequestAsync(path, ct).ConfigureAwait(false);

                if (await response.DeserializeContentAsync<UserProfile>(ct).ConfigureAwait(false) is UserProfile userProfile)
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
