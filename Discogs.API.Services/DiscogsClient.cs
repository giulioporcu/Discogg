using Discogs.API.Framework.Events;
using Discogs.API.Framework.Extensions;
using Discogs.API.Framework.Services;
using Discogs.API.Framework.Utility;
using Microsoft.Extensions.Logging;

namespace Discogs.API.Framework
{
    /// <summary>
    /// Provides low‑level HTTP request handling for communicating with the Discogs API,
    /// including URI construction, request execution, and request lifecycle events.
    /// </summary>
    public class DiscogsClient
    {
        public const string ROOT = "https://api.discogs.com";

        /// <summary>
        /// Occurs when an error is raised during request processing.
        /// The event argument contains a descriptive error message.
        /// </summary>
        public event EventHandler<string>? OnError;
        internal void TriggerErrorEvent(object sender, string message) => this.OnError?.Invoke(sender, message);

        /// <summary>
        /// Occurs immediately before an HTTP request is sent.
        /// Provides information about the HTTP method and target URI.
        /// </summary>
        public event EventHandler<HttpRequestStartingEventArgs>? OnHttpRequestStarting;

        /// <summary>
        /// Occurs after an HTTP request completes.
        /// Provides information about the HTTP method, target URI, and response status code.
        /// </summary>
        public event EventHandler<HttpRequestCompletedEventArgs>? OnHttpRequestCompleted;


        private HttpClient HttpClient { get; }
        private IList<ILogger> Loggers { get; } = [];

        public UserService UserService { get; }
        public VersionInfoService ApiVersionInfoService { get; }
        public JsonSerializationService JsonSerializationService { get; }
        public AuthenticationService AuthenticationService { get; }


        /// <summary>
        /// Initializes a new instance with default headers and logging.
        /// </summary>
        /// <param name="settings">The application configuration providing API base URL and retry settings.</param>
        /// </param>
        public DiscogsClient(HttpClient client)
        {
            this.HttpClient = client;
            this.JsonSerializationService = new JsonSerializationService(this);
            this.ApiVersionInfoService = new VersionInfoService(this, this.JsonSerializationService);
            this.UserService = new UserService(this, this.JsonSerializationService);
            this.AuthenticationService = new AuthenticationService(this, this.JsonSerializationService);

            string product = CurrentAssemblyInfo.GetProduct();
            string copyright = CurrentAssemblyInfo.GetCopyright();
            this.HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"{product} / {copyright} / {UserAgents.Generate()}");
        }

        public void RegisterLogger(ILogger logger) => this.Loggers.Add(logger);


        /// <summary>
        /// Assembles a fully qualified Discogs API URI using the specified route
        /// and optional query parameters.
        /// </summary>
        /// <param name="route">The API route to append to the base URI.</param>
        /// <param name="queryParams">Optional query parameters to include in the URI.</param>
        /// <returns>A complete request URI as a string.</returns>
        public static string AssembleFullUrl(string? route, Dictionary<string, string>? queryParams = null)
        {
            UriBuilder uriBuilder = new($"https://api.discogs.com{route ?? String.Empty}");

            if (queryParams is null || queryParams.Count <= 0)
            {
                return uriBuilder.Uri.ToString();
            }

            string query = String.Join("&", queryParams
                .Where(pair => !String.IsNullOrWhiteSpace(pair.Value))
                .Select(pair => $"{pair.Key}={Uri.EscapeDataString(pair.Value!)}"));

            uriBuilder.Query = query;
            return uriBuilder.Uri.ToString();
        }

        public async Task<HttpResponseMessage> DoRequestAsync(HttpMethod method, string uriString, HttpContent? content, CancellationToken ct)
        {
            try
            {
                Uri uri = new(uriString);
                using HttpRequestMessage request = new(method, uri) { Content = content };
                this.OnHttpRequestStarting?.Invoke(this, new HttpRequestStartingEventArgs(method, uri));
                using HttpResponseMessage responseMessage = await this.HttpClient.SendAsync(request, ct).ConfigureAwait(false);
                this.OnHttpRequestCompleted?.Invoke(this, new HttpRequestCompletedEventArgs(method, uri, responseMessage.StatusCode));

                if (responseMessage.IsSuccessStatusCode)
                {
                    return responseMessage;
                }
                else
                {
                    responseMessage.Dispose();
                    throw new Exception("Request did not return a success status.");
                }
            }
            catch (Exception exception)
            {
                foreach (ILogger logger in this.Loggers)
                {
                    logger.LogError($"An exception occured when calling '{uriString}': {exception.Message}");
                }

                throw;
            }
        }

        public async Task<HttpResponseMessage> DoGetRequestAsync(string? path = null, CancellationToken ct = default)
            => await this.DoRequestAsync(HttpMethod.Get, $"{ROOT}{path?.EnsureStartsWith("/")}", null, ct);
    }
}