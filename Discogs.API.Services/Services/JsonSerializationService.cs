using System.Text.Json;

namespace Discogs.API.Framework.Services
{
    public class JsonSerializationService(DiscogsClient client)
    {
        private DiscogsClient Client { get; } = client;

        public async Task<T?> DeserializeContentAsync<T>(HttpResponseMessage responseMessage, CancellationToken ct = default)
        {
            try
            {
                using Stream stream = await responseMessage.Content.ReadAsStreamAsync(ct);
                return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: ct);
            }
            catch (Exception exception)
            {
                this.Client.TriggerErrorEvent(this, exception.Message);
                return default;
            }
        }
    }
}