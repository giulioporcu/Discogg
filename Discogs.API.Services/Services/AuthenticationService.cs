using Discogs.API.Core;

namespace Discogs.API.Framework.Services
{
    /// <summary>
    /// Handles OAuth authentication with the Discogs API.
    /// </summary>
    public class AuthenticationService(DiscogsClient discogsClient, JsonSerializationService jsonSerializationService)
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
        /// Retrieves the authenticated user's OAuth identity.
        /// </summary>
        /// <param name="apiToken">The API authentication token.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The authenticated user's OAuth information.</returns>
        /// <exception cref="ArgumentException"><paramref name="apiToken"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException ">The API request or deserialization failed.</exception>
        public async Task<OAuth> GetAuthenticatedUser(string apiToken, CancellationToken ct = default)
        {
            if (String.IsNullOrWhiteSpace(apiToken))
            {
                throw new ArgumentException("Authentication attempt with empty token.");
            }

            try
            {
                string uri = DiscogsClient.AssembleFullUrl("/oauth/identity", new Dictionary<string, string>() { { "token", apiToken } });
                using HttpResponseMessage response = await this.DiscogsClient.DoRequestAsync(HttpMethod.Get, uri, content: null, ct);
                return await this.JsonSerializationService.DeserializeContentAsync<OAuth>(response, ct)
                       ?? throw new InvalidOperationException("Current user cannot be authenticated.");
            }
            catch (Exception exception)
            {
                this.DiscogsClient.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}