using Discogs.API.Core;

namespace Discogs.API.Framework.Services
{
    public class AuthenticationService(DiscogsClient discogsClient, JsonSerializationService jsonSerializationService)
    {
        private DiscogsClient DiscogsClient { get; } = discogsClient;
        private JsonSerializationService JsonSerializationService { get; } = jsonSerializationService;

        public async Task<OAuth> GetAuthenticatedUser(string apiToken, CancellationToken ct = default)
        {
            if (String.IsNullOrWhiteSpace(apiToken))
            {
                throw new InvalidOperationException("Authentication attempt with empty token.");
            }

            try
            {
                string uri = DiscogsClient.AssembleFullUrl("/oauth/identity", new Dictionary<string, string>() { { "token", apiToken } });
                using HttpResponseMessage response = await this.DiscogsClient.DoRequestAsync(HttpMethod.Get, uri, content: null, ct);
                return await this.JsonSerializationService.DeserializeContentAsync<OAuth>(response, ct)
                       ?? throw new Exception("Current user canont be authenticated.");
            }
            catch (Exception exception)
            {
                this.DiscogsClient.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}