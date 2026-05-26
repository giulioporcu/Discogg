using Discogs.API.Services.Events;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Reflection;

namespace Discogs.API.Services
{
    /// <summary>
    /// Provides low‑level HTTP request handling for communicating with the Discogs API,
    /// including URI construction, request execution, and request lifecycle events.
    /// </summary>
    public class DiscogsService
    {
        /// <summary>
        /// Occurs when an error is raised during request processing.
        /// The event argument contains a descriptive error message.
        /// </summary>
        public event EventHandler<string>? OnError;

        /// <summary>
        /// Occurs immediately before an HTTP request is sent.
        /// Provides information about the HTTP method and target URI.
        /// </summary>
        public event EventHandler<RequestStartingEventArgs>? OnRequestStarting;

        /// <summary>
        /// Occurs after an HTTP request completes.
        /// Provides information about the HTTP method, target URI, and response status code.
        /// </summary>
        public event EventHandler<RequestCompletedEventArgs>? OnRequestCompleted;

        private readonly HttpClient _client;
        private readonly ApplicationConfig _config;
        private readonly ILogger<DiscogsService>? _logger; 

        /// <summary>
        /// Initializes a new instance with default headers and logging.
        /// </summary>
        /// <param name="config">The application configuration providing API base URL and retry settings.</param>
        /// <param name="httpClientFactory">The factory used to create <see cref="HttpClient"/> instances.</param>
        /// <param name="logger">
        /// Optional logger used to record request lifecycle and errors.
        /// </param>
        public DiscogsService(ApplicationConfig config, IHttpClientFactory httpClientFactory, ILogger<DiscogsService>? logger = null)
        {
            this._client = httpClientFactory.CreateClient();
            this._config = config;
            this._logger = logger;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string name = assembly.GetName().Name ?? String.Empty;
            string versionNumber = assembly.GetName().Version?.ToString() ?? String.Empty;

            this._client.DefaultRequestHeaders.UserAgent.ParseAdd($"{name}/{versionNumber}");
        }

        /// <summary>
        /// Assembles a fully qualified Discogs API URI using the specified route
        /// and optional query parameters.
        /// </summary>
        /// <param name="route">The API route to append to the base URI.</param>
        /// <param name="queryParams">Optional query parameters to include in the URI.</param>
        /// <returns>A complete request URI as a string.</returns>
        public string AssembleUri(string? route, Dictionary<string, string>? queryParams = null)
        {
            UriBuilder uriBuilder = new($"{this._config.BaseUrl}{route ?? String.Empty}");

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

        /// <summary>
        /// Determines whether the HTTP response indicates a transient error
        /// that may succeed on retry.
        /// </summary>
        private static bool IsTransientError(HttpResponseMessage response) => response.StatusCode switch
        {
            HttpStatusCode.ServiceUnavailable => true,
            HttpStatusCode.TooManyRequests => true,
            HttpStatusCode.GatewayTimeout => true,
            HttpStatusCode.BadGateway => true,
            _ => (int)response.StatusCode >= 500
        };

        /// <summary>
        /// Sends an HTTP request to the specified URI using the given method and content,
        /// with automatic retry logic for transient errors.
        /// </summary>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="uriString">The target URI as a string.</param>
        /// <param name="content">Optional HTTP content to include in the request.</param>
        /// <param name="ct">A cancellation token used to cancel the request.</param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/> returned by the server,
        /// or <c>null</c> if an error occurred.
        /// </returns>
        public async Task<HttpResponseMessage?> DoRequestAsync(HttpMethod method, string uriString, HttpContent? content, CancellationToken ct)
        {
            Uri uri = new(uriString);
            this.OnRequestStarting?.Invoke(null, new RequestStartingEventArgs(method, uri));

            Exception? lastException = null;
            int maxRetries = this._config.MaxRetries;
            int initialDelayMs = this._config.InitialDelayMs;

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    using HttpRequestMessage request = new(method, uri) { Content = content };
                    using HttpResponseMessage response = await this._client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);

                    this.OnRequestCompleted?.Invoke(null, new RequestCompletedEventArgs(method, uri, response.StatusCode));

                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }

                    if (!IsTransientError(response))
                    {
                        this._logger?.LogWarning("Non-retryable error: {StatusCode}", response.StatusCode);
                        return response;
                    }

                    this._logger?.LogWarning("Transient error {StatusCode}, attempt {Attempt}/{MaxRetries}",
                        response.StatusCode, attempt + 1, maxRetries);

                    await Task.Delay(initialDelayMs * (int)Math.Pow(2, attempt), ct).ConfigureAwait(false);
                }
                catch (Exception exception) when (attempt < maxRetries - 1)
                {
                    lastException = exception;
                    this._logger?.LogWarning(exception, "Request failed, attempt {Attempt}/{MaxRetries}", attempt + 1, maxRetries);
                    await Task.Delay(initialDelayMs * (int)Math.Pow(2, attempt), ct).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    lastException = exception;
                    this._logger?.LogError(exception, "Request failed after {MaxRetries} attempts", maxRetries);
                }
            }

            string errorMessage = lastException?.Message ?? "Unknown error after retries";
            this.OnError?.Invoke(this, errorMessage);
            return null;
        }

        /// <summary>
        /// Sends a GET request to the specified URI and returns the response.
        /// </summary>
        /// <param name="uriString">The target URI.</param>
        /// <param name="ct">A cancellation token used to cancel the request.</param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/> returned by the server,
        /// or <c>null</c> if an error occurred.
        /// </returns>
        public async Task<HttpResponseMessage?> DoGetRequestAsync(string uriString, CancellationToken ct = default)
            => await this.DoRequestAsync(HttpMethod.Get, uriString, content: null, ct).ConfigureAwait(false);
    }
}