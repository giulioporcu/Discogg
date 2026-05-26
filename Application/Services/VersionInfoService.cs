using Application.Properties;
using Discogs.API.Core;
using Discogs.API.Services;
using Discogs.API.Services.Events;
using System.Reflection;

namespace Application.Services
{
    /// <summary>
    /// Provides functionality for retrieving version information from both the application
    /// assembly and the Discogs API, including connectivity status tracking and error reporting.
    /// </summary>
    /// <param name="discogsService">
    /// The service responsible for performing HTTP requests to the Discogs API.
    /// </param>
    /// <param name="serializationService">
    /// The service used to deserialize JSON responses returned by the API.
    /// </param>
    public class VersionInfoService(DiscogsService discogsService, SerializationService serializationService)
    {
        /// <summary>
        /// Occurs when an error is raised during version retrieval or connectivity checks.
        /// The event argument contains a descriptive error message.
        /// </summary>
        public event EventHandler<string>? OnError;

        /// <summary>
        /// Occurs when the connectivity state changes, such as when API version
        /// information is successfully retrieved or reset.
        /// </summary>
        public event EventHandler<ConnectivityChangeEventArgs>? OnConnectivityChanged;

        /// <summary>
        /// Occurs when the application version has been successfully retrieved
        /// and normalized from assembly metadata.
        /// </summary>
        public event EventHandler<string>? OnApplicationVersionFetched;

        /// <summary>
        /// Gets the version information returned by the Discogs API,
        /// or <c>null</c> if no information has been retrieved.
        /// </summary>
        public ApiVersionInfo? ApiInfo { get; private set; }

        /// <summary>
        /// Gets the normalized application version extracted from assembly metadata,
        /// or <c>null</c> if it has not been retrieved.
        /// </summary>
        public string? ApplicationVersion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the service is currently connected
        /// to the Discogs API based on retrieved version information.
        /// </summary>
        public bool IsConnected => !String.IsNullOrWhiteSpace(this.ApiInfo?.ApiVersion);

        /// <summary>
        /// The underlying Discogs service used to perform HTTP requests.
        /// </summary>
        private readonly DiscogsService _discogsService = discogsService;

        /// <summary>
        /// The serialization service used to deserialize JSON responses.
        /// </summary>
        private readonly SerializationService _serializationService = serializationService;

        /// <summary>
        /// Retrieves the application version from assembly metadata, normalizes it,
        /// stores it, and raises an event when successfully fetched.
        /// </summary>
        /// <param name="ct">A cancellation token used to cancel the operation.</param>
        /// <returns>
        /// The normalized application version string if retrieval succeeds;
        /// otherwise <c>null</c>.
        /// </returns>
        public async Task<string?> GetApplicationVersionAsync(CancellationToken ct = default)
        {
            this.ApplicationVersion = null;

            try
            {
                ct.ThrowIfCancellationRequested();

                Assembly? assembly = Assembly.GetExecutingAssembly();
                if (assembly is null)
                {
                    return await Task.FromResult((string?)null);
                }

                AssemblyInformationalVersionAttribute? attribute = assembly
                    .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                    .OfType<AssemblyInformationalVersionAttribute>()
                    .FirstOrDefault();

                string? raw = attribute?.InformationalVersion;

                if (String.IsNullOrWhiteSpace(raw))
                {
                    return await Task.FromResult((string?)null);
                }

                string numericPart = raw.Split('-', '+')[0];

                if (!Version.TryParse(numericPart, out Version? version))
                {
                    return await Task.FromResult((string?)null);
                }

                string normalized = $"{version.Major}.{version.Minor}";

                this.ApplicationVersion = normalized;
                this.OnApplicationVersionFetched?.Invoke(this, normalized);
                return await Task.FromResult(normalized);
            }
            catch (Exception exception)
            {
                this.OnError?.Invoke(this, exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieves version information from the Discogs API root endpoint.
        /// Updates connectivity state and raises related events.
        /// </summary>
        /// <param name="ct">A cancellation token used to cancel the request.</param>
        /// <returns>
        /// An <see cref="ApiVersionInfo"/> instance if the request and deserialization succeed;
        /// otherwise <c>null</c>.
        /// </returns>
        public async Task<ApiVersionInfo?> GetApiInfoAsync(CancellationToken ct = default)
        {
            this.ApiInfo = null;
            this.OnConnectivityChanged?.Invoke(this, new ConnectivityChangeEventArgs(null));

            try
            {
                string url = this._discogsService.AssembleUri(String.Empty);
                using HttpResponseMessage? responseMessage = await this._discogsService.DoRequestAsync(HttpMethod.Get, url, content: null, ct) ?? throw new Exception(Messages.WebRequestFailed);

                if (responseMessage.IsSuccessStatusCode && await this._serializationService.DeserializeAsync<ApiVersionInfo>(responseMessage, ct) is ApiVersionInfo connectionInfo)
                {
                    this.ApiInfo = connectionInfo;
                    this.OnConnectivityChanged?.Invoke(this, new ConnectivityChangeEventArgs(connectionInfo));
                }
            }
            catch (Exception exception)
            {
                this.OnError?.Invoke(this, exception.Message);
            }

            return this.ApiInfo;
        }
    }
}