namespace Discogs.API.Framework.Events
{
    /// <summary>
    /// Provides data for the <see cref="DiscogsClient.OnHttpRequestStarting"/> event.
    /// </summary>
    public class HttpRequestStartingEventArgs(HttpMethod method, Uri uri) : EventArgs
    {
        /// <summary>
        /// Gets the HTTP method used for the request.
        /// </summary>
        public HttpMethod Method { get; } = method;

        /// <summary>
        /// Gets the target URI of the request.
        /// </summary>
        public Uri Uri { get; } = uri;
    }
}