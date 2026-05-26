using Discogs.API.Core;
using Discogs.API.Services.Events;
using Microsoft.Extensions.Logging;

namespace Discogs.API.Services
{
    /// <summary>
    /// Provides authentication functionality for validating Discogs API tokens,
    /// retrieving the associated user identity, and managing authentication state.
    /// </summary>
    /// <param name="discogsService">
    /// The service used to perform authenticated Discogs API requests.
    /// </param>
    /// <param name="serializationService">
    /// The service responsible for deserializing API responses.
    /// </param>
    /// <param name="logger">
    /// The <see cref="ILogger{AuthenticationService}"/> instance used for logging.
    /// </param>
    public class AuthenticationService(DiscogsService discogsService, SerializationService serializationService, ILogger<AuthenticationService>? logger = null)
    {
        /// <summary>
        /// Occurs when an authentication-related error is raised.
        /// The event argument contains a descriptive error message.
        /// </summary>
        public event EventHandler<string>? OnError;

        /// <summary>
        /// Occurs when the authentication state changes, such as after a successful
        /// login, logout, or failed authentication attempt.
        /// </summary>
        public event EventHandler<AuthenticationChangedEventArgs>? OnAuthenticationChanged;

        /// <summary>
        /// Gets the authenticated user information returned by the Discogs API,
        /// or <c>null</c> if no user is currently authenticated.
        /// </summary>
        public OAuth? User { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a user is currently authenticated.
        /// </summary>
        public bool IsAuthenticated => this.User?.Id != null;

        private readonly ILogger<AuthenticationService>? _logger = logger;

        /// <summary>
        /// Attempts to authenticate using the provided API token. If successful,
        /// retrieves the associated user identity from the Discogs API and updates
        /// the authentication state. Errors and state changes are surfaced through events.
        /// </summary>
        /// <param name="token">The Discogs API token to authenticate with.</param>
        /// <param name="ct">A cancellation token used to cancel the request.</param>
        /// <returns>
        /// The authenticated <see cref="OAuth"/> user object if authentication succeeds;
        /// otherwise <c>null</c>.
        /// </returns>
        public async Task<OAuth?> AuthenticateAsync(string token, CancellationToken ct = default)
        {
            this.User = null;
            this.OnAuthenticationChanged?.Invoke(this, new AuthenticationChangedEventArgs(null));

            if (String.IsNullOrWhiteSpace(token))
            {
                this._logger?.LogWarning("Authentication attempt with empty token");
                this.OnError?.Invoke(this, "Authentication attempt with empty token");
                return null;
            }

            try
            {
                string uri = discogsService.AssembleUri("/oauth/identity", new Dictionary<string, string>() { { "token", token } });
                using HttpResponseMessage? response = await discogsService.DoRequestAsync(HttpMethod.Get, uri, content: null, ct);

                if (response is null)
                {
                    this._logger?.LogError("Web request failed");
                    this.OnError?.Invoke(this, "Web request failed");
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    OAuth? user = await serializationService.DeserializeAsync<OAuth>(response, ct);
                    if (user is OAuth oauthUser)
                    {
                        oauthUser.Token = token;
                        this.User = oauthUser;
                        this._logger?.LogInformation("User authenticated: {Username}", oauthUser.UserName);
                        this.OnAuthenticationChanged?.Invoke(this, new AuthenticationChangedEventArgs(this.User));
                        return this.User;
                    }
                }

                string errorContent = await response.Content.ReadAsStringAsync(ct);
                this._logger?.LogWarning("Authentication failed: {Error}", errorContent);
                this.OnError?.Invoke(this, errorContent);
            }
            catch (Exception exception)
            {
                this._logger?.LogError(exception, "Authentication error");
                this.OnError?.Invoke(this, exception.Message);
            }

            return null;
        }
    }
}