using Discogs.API.Core;
using Discogs.API.Framework.Extensions;

namespace Discogs.API.Framework.Services
{
    /// <summary>
    /// Handles OAuth authentication with the Discogs API.
    /// </summary>
    public class AuthenticationService(DiscogsClient discogsClient)
    {
        /// <summary>
        /// Gets the Discogs client for API communication.
        /// </summary>
        private DiscogsClient DiscogsClient { get; } = discogsClient;

        /// <summary>
        /// Gets the dictionary of cached authenticated users keyed by API token.
        /// </summary>
        private Dictionary<string, OAuth> AuthenticatedUsers { get; } = [];

        /// <summary>
        /// Retrieves the authenticated user's OAuth identity.
        /// </summary>
        /// <param name="apiToken">The API authentication token.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The authenticated user's OAuth information.</returns>
        /// <exception cref="ArgumentException"><paramref name="apiToken"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">The API request or deserialization failed.</exception>
        public async Task<OAuth> GetAuthenticatedUser(string apiToken, CancellationToken ct = default)
        {
            if (String.IsNullOrWhiteSpace(apiToken))
            {
                throw new ArgumentException("Authentication attempt with empty token.");
            }

            if (this.AuthenticatedUsers.TryGetValue(apiToken, out OAuth? existing))
            {
                return existing;
            }

            try
            {
                string path = $"/oauth/identity?token={Uri.EscapeDataString(apiToken)}";
                using HttpResponseMessage response = await this.DiscogsClient.DoGetRequestAsync(path, ct).ConfigureAwait(false);

                if (await response.DeserializeContentAsync<OAuth>(ct).ConfigureAwait(false) is OAuth oauth)
                {
                    this.AuthenticatedUsers.Add(apiToken, oauth);
                    return oauth;
                }

                throw new InvalidOperationException("Current user cannot be authenticated.");

            }
            catch (Exception exception)
            {
                this.DiscogsClient.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}