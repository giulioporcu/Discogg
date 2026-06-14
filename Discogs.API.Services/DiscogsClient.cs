using Discogs.API.Framework.Events;
using Discogs.API.Framework.Extensions;
using Discogs.API.Framework.Services;
using Discogs.API.Framework.Utility;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Discogs.API.Framework
{
    /// <summary>
    /// Provides low‑level HTTP request handling for communicating with the Discogs API,
    /// including URI construction, request execution, and request lifecycle events.
    /// </summary>
    public class DiscogsClient
    {
        /// <summary>
        /// The base URL for the Discogs API.
        /// </summary>
        public const string ROOT = "https://api.discogs.com";

        /// <summary>
        /// Occurs when an error is raised during request processing.
        /// The event argument contains a descriptive error message.
        /// </summary>
        public event EventHandler<Events.ErrorEventArgs>? OnError;

        /// <summary>
        /// Raises the <see cref="OnError"/> event.
        /// </summary>
        /// <param name="sender">The source of the error.</param>
        /// <param name="message">The error message.</param>
        internal void TriggerErrorEvent(object sender, string message)
        {
            foreach (ILogger logger in this.Loggers)
            {
                logger.LogError("An error occurred: {message}", message);
            }

            this.OnError?.Invoke(sender, new Events.ErrorEventArgs(message));
        }

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

        /// <summary>
        /// Gets the underlying HTTP client for API communication.
        /// </summary>
        private HttpClient HttpClient { get; }

        /// <summary>
        /// Gets the list of registered loggers.
        /// </summary>
        private IList<ILogger> Loggers { get; } = [];

        /// <summary>
        /// Gets the user service for profile operations.
        /// </summary>
        public UserService UserService { get; }

        /// <summary>
        /// Gets the version info service for API version queries.
        /// </summary>
        public VersionInfoService VersionInfoService { get; }

        /// <summary>
        /// Gets the authentication service.
        /// </summary>
        public AuthenticationService AuthenticationService { get; }

        /// <summary>
        /// Initializes a new instance with default headers and logging.
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> configured for API communication.</param>
        public DiscogsClient(HttpClient client)
        {
            this.HttpClient = client;
            this.VersionInfoService = new VersionInfoService(this);
            this.UserService = new UserService(this);
            this.AuthenticationService = new AuthenticationService(this);

            string product = CurrentAssemblyInfo.Product.Value;
            string copyright = CurrentAssemblyInfo.Copyright.Value;
            this.HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"{product}/{copyright}");
        }

        /// <summary>
        /// Registers a logger to receive error logs from API requests.
        /// </summary>
        /// <param name="logger">The logger instance to register.</param>
        public void RegisterLogger(ILogger logger) => this.Loggers.Add(logger);

        /// <summary>
        /// Assembles the query parameter path starting with ?.
        /// </summary>
        /// <param name="queryParams">Optional query parameters to include in the URI.</param>
        /// <returns>The query string starting with ?, or empty if no parameters.</returns>
        public static string AssembleQueryParameters(Dictionary<string, string>? queryParams = null)
        {
            if (queryParams is null || queryParams.Count <= 0)
            {
                return String.Empty;
            }

            StringBuilder stringBuilder = new();

            bool firstParameter = true;
            foreach (KeyValuePair<string, string> pair in queryParams)
            {
                if (String.IsNullOrWhiteSpace(pair.Value))
                {
                    continue;
                }

                stringBuilder.Append(firstParameter ? '?' : '&');
                firstParameter = false;

                stringBuilder.Append(pair.Key).Append('=').Append(Uri.EscapeDataString(pair.Value));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Sends an HTTP request and returns the response.
        /// </summary>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="uriString">The target URI string.</param>
        /// <param name="content">Optional HTTP content for the request body.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The HTTP response message.</returns>
        /// <exception cref="HttpRequestException">The API request ran into an error.</exception>
        /// <exception cref="UriFormatException">If the uri string is malformed.</exception>
        /// <exception cref="OperationCanceledException">If the request was canceled.</exception>
        public async Task<HttpResponseMessage> DoRequestAsync(HttpMethod method, string uriString, HttpContent? content, CancellationToken ct)
        {
            try
            {
                Uri uri = new(uriString);
                using HttpRequestMessage request = new(method, uri) { Content = content };
                this.OnHttpRequestStarting?.Invoke(this, new HttpRequestStartingEventArgs(method, uri));
                HttpResponseMessage responseMessage = await this.HttpClient.SendAsync(request, ct).ConfigureAwait(false);
                this.OnHttpRequestCompleted?.Invoke(this, new HttpRequestCompletedEventArgs(method, uri, responseMessage.StatusCode));

                return responseMessage;
            }
            catch (Exception exception)
            {
                this.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }

        /// <summary>
        /// Sends a GET request to the Discogs API.
        /// </summary>
        /// <param name="path">Optional path to append to the base API URL.</param>
        /// <param name="ct">Cancellation token for the request.</param>
        /// <returns>The HTTP response message.</returns>
        public async Task<HttpResponseMessage> DoGetRequestAsync(string? path = null, CancellationToken ct = default)
            => await this.DoRequestAsync(HttpMethod.Get, $"{ROOT}{path?.EnsureStartsWith("/")}", null, ct).ConfigureAwait(false);
    }
}