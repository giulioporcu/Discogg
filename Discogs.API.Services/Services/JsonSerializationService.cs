using System.Text.Json;

namespace Discogs.API.Framework.Services
{
    /// <summary>
    /// Provides JSON serialization and deserialization for API responses.
    /// </summary>
    public class JsonSerializationService(DiscogsClient client)
    {
        /// <summary>
        /// Gets the Discogs client for error reporting.
        /// </summary>
        private DiscogsClient Client { get; } = client;

        /// <summary>
        /// Deserializes the HTTP response content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize into.</typeparam>
        /// <param name="responseMessage">The HTTP response containing the JSON content.</param>
        /// <param name="ct">Cancellation token for the operation.</param>
        /// <returns>The deserialized object, or default if deserialization fails.</returns>
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
                throw;
            }
        }
    }
}