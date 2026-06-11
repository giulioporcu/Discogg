using Discogs.API.Core;
using Discogs.API.Framework.Utility;

namespace Discogs.API.Framework.Services
{
    /// <summary>
    /// Provides application and API version information.
    /// </summary>
    public class VersionInfoService(DiscogsClient client, JsonSerializationService serializationService)
    {
        /// <summary>
        /// Gets the Discogs client for API communication.
        /// </summary>
        private DiscogsClient Client { get; } = client;

        /// <summary>
        /// Gets the JSON serialization service.
        /// </summary>
        private JsonSerializationService JsonSerializationService { get; } = serializationService;

        /// <summary>
        /// Gets the current application version from assembly metadata.
        /// </summary>
        /// <returns>The application version string.</returns>
        public string GetApplicationVersion()
        {
            try
            {
                return CurrentAssemblyInfo.GetVersion();
            }
            catch (Exception exception)
            {
                this.Client.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves version information from the Discogs API.
        /// </summary>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The API version information.</returns>
        /// <exception cref="InvalidOperationException">The API response could not be deserialized.</exception>
        public async Task<ApiVersionInfo> GetDiscogsVersionAsync(CancellationToken ct = default)
        {
            try
            {
                using HttpResponseMessage responseMessage = await this.Client.DoGetRequestAsync(path: null, ct);

                return await this.JsonSerializationService.DeserializeContentAsync<ApiVersionInfo>(responseMessage, ct)
                    ?? throw new InvalidOperationException("Unable to parse Discogs response.");
            }
            catch (InvalidOperationException exception)
            {
                this.Client.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}