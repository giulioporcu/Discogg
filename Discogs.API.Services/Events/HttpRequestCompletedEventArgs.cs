using System.Net;

namespace Discogs.API.Framework.Events
{
    public class HttpRequestCompletedEventArgs(HttpMethod method, Uri uri, HttpStatusCode statusCode) : EventArgs
    {
        public HttpMethod Method { get; } = method;

        public Uri Uri { get; } = uri;

        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}