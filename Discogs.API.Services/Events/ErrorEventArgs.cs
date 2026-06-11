namespace Discogs.API.Framework.Events
{
    /// <summary>
    /// Provides data for the <see cref="DiscogsClient.OnError"/> event.
    /// </summary>
    public class ErrorEventArgs(string errorMessage) : EventArgs
    {
        /// <summary>
        /// Gets the error description message.
        /// </summary>
        public string ErrorMessage { get; } = errorMessage;
    }
}
