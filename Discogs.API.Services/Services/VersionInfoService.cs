using Discogs.API.Core;
using Discogs.API.Framework.Extensions;
using Discogs.API.Framework.Utility;

namespace Discogs.API.Framework.Services
{
    /// <summary>
    /// Provides application and API version information.
    /// </summary>
    public class VersionInfoService(DiscogsClient client)
    {
        /// <summary>
        /// Gets the Discogs client for API communication.
        /// </summary>
        private DiscogsClient Client { get; } = client;

        /// <summary>
        /// Gets the current application version from assembly metadata.
        /// </summary>
        /// <returns>The application version string.</returns>
        /// <exception cref="InvalidOperationException">The assembly version attribute is missing, empty, or unparsable.</exception>
        public string GetApplicationVersion()
        {
            try
            {
                return CurrentAssemblyInfo.Version.Value;
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
        /// <exception cref="InvalidDataException">The API response could not be deserialized.</exception>
        public async Task<ApiVersionInfo> GetDiscogsVersionAsync(CancellationToken ct = default)
        {
            try
            {
                using HttpResponseMessage response = await this.Client.DoGetRequestAsync(path: null, ct).ConfigureAwait(false);

                return await response.DeserializeContentAsync<ApiVersionInfo>(ct).ConfigureAwait(false) ?? throw new InvalidDataException("Unable to parse Discogs response.");
            }
            catch (InvalidDataException exception)
            {
                this.Client.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}