using Discogs.API.Core;

namespace Discogs.API.Framework.Services
{
    public class UserService(DiscogsClient discogsClient, JsonSerializationService jsonSerializationService)
    {
        private DiscogsClient DiscogsClient { get; } = discogsClient;
        private JsonSerializationService JsonSerializationService { get; } = jsonSerializationService;

        public async Task<UserProfile?> GetProfileAsync(string userName, CancellationToken ct = default)
        {
            try
            {
                using HttpResponseMessage responseMessage = await this.DiscogsClient.DoGetRequestAsync($"/users/{userName}", ct);
                return await this.JsonSerializationService.DeserializeContentAsync<UserProfile>(responseMessage, ct) is UserProfile user ? user : null;
            }
            catch (Exception exception)
            {
                this.DiscogsClient.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}
