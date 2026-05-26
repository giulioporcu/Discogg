using Discogs.API.Core;

namespace Discogs.API.Services.Events
{
    /// <summary>
    /// Provides data for authentication state changes.
    /// </summary>
    /// <param name="oAuth">
    /// The authenticated OAuth user, or <c>null</c> if authentication was cleared.
    /// </param>
    public class AuthenticationChangedEventArgs(OAuth? oAuth = null) : EventArgs
    {
        /// <summary>
        /// Gets a value indicating whether a user is authenticated.
        /// </summary>
        public bool IsAuthenticated => this.OAuth?.Id != null;

        /// <summary>
        /// Gets the OAuth user associated with the authentication change.
        /// </summary>
        public OAuth? OAuth { get; } = oAuth;
    }
}