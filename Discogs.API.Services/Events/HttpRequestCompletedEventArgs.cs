using System.Net;

namespace Discogs.API.Framework.Events
{
    /// <summary>
    /// Provides data for the <see cref="DiscogsClient.OnHttpRequestCompleted"/> event.
    /// </summary>
    public class HttpRequestCompletedEventArgs(HttpMethod method, Uri uri, HttpStatusCode statusCode) : EventArgs
    {
        /// <summary>
        /// Gets the HTTP method used for the request.
        /// </summary>
        public HttpMethod Method { get; } = method;

        /// <summary>
        /// Gets the target URI of the request.
        /// </summary>
        public Uri Uri { get; } = uri;

        /// <summary>
        /// Gets the HTTP status code returned by the API.
        /// </summary>
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}