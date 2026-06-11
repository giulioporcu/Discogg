namespace Discogs.API.Framework.Events
{
    public class HttpRequestStartingEventArgs(HttpMethod method, Uri uri) : EventArgs
    {
        public HttpMethod Method { get; } = method;

        public Uri Uri { get; } = uri;
    }
}