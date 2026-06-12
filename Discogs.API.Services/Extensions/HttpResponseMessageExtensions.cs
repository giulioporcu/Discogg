using System.Text.Json;

namespace Discogs.API.Framework.Extensions
{
    /// <summary>
    /// Provides extension methods for deserializing HTTP response content.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserializes the HTTP response content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize into.</typeparam>
        /// <param name="response">The HTTP response containing the JSON content.</param>
        /// <param name="ct">Cancellation token for the operation.</param>
        /// <returns>The deserialized object, or default if deserialization fails.</returns>
        public static async Task<T?> DeserializeContentAsync<T>(this HttpResponseMessage response, CancellationToken ct = default)
            => await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false), cancellationToken: ct).ConfigureAwait(false);
    }
}
