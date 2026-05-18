using Discogs.API.Core;

namespace Discogs.API.Services
{
    /// <summary>
    /// Provides access to Discogs user information, including retrieval of user profiles
    /// and error reporting when requests or deserialization fail.
    /// </summary>
    /// <param name="discogsService">
    /// The service responsible for performing HTTP requests to the Discogs API.
    /// </param>
    /// <param name="serializationService">
    /// The service used to deserialize JSON responses returned by the API.
    /// </param>
    public class UserService(DiscogsService discogsService, SerializationService serializationService)
    {
        /// <summary>
        /// Occurs when an error is raised during user retrieval.
        /// The event argument contains a descriptive error message.
        /// </summary>
        public event EventHandler<string>? OnError;

        /// <summary>
        /// The underlying Discogs service used to perform HTTP requests.
        /// </summary>
        private readonly DiscogsService _discogsService = discogsService;

        /// <summary>
        /// The serialization service used to deserialize JSON responses.
        /// </summary>
        private readonly SerializationService _serializationService = serializationService;

        /// <summary>
        /// Retrieves a Discogs user profile by username.
        /// Sends a GET request to the Discogs API and deserializes the response
        /// into a <see cref="UserProfile"/> model if successful.
        /// </summary>
        /// <param name="name">The username of the Discogs user to retrieve.</param>
        /// <param name="ct">A cancellation token used to cancel the request.</param>
        /// <returns>
        /// A <see cref="UserProfile"/> instance if the request and deserialization succeed;
        /// otherwise <c>null</c>.
        /// </returns>
        public async Task<UserProfile?> GetAsync(string name, CancellationToken ct = default)
        {
            try
            {
                string uri = DiscogsService.AssembleUri($"/users/{name}");

                if (await this._discogsService.DoGetRequestAsync(uri, ct) is HttpResponseMessage userResponse)
                {
                    using (userResponse)
                    {
                        if (userResponse.IsSuccessStatusCode && await this._serializationService.DeserializeAsync<UserProfile>(userResponse, ct) is UserProfile user)
                        {
                           // user.Listings = await this.Hydrate<List<Listing>>(user.InventoryUrl, ct) ?? [];
                         //   user.CollectionFolders = await this.Hydrate<List<UserCollectionFolder>>(user.CollectionFoldersUrl, ct) ?? [];
                        //    user.CollectionFields = await this.Hydrate<List<UserCollectionField>>(user.CollectionFieldsUrl, ct) ?? [];

                            return user;
                        }
                    }
                }

                return null;
            }
            catch (Exception exception)
            {
                this.OnError?.Invoke(this, exception.Message);
                return null;
            }
        }

        public async Task<T?> Hydrate<T>(string? url, CancellationToken ct)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                // TODO localize
                throw new Exception("No valid url");
            }

            if (await this._discogsService.DoGetRequestAsync(url, ct) is HttpResponseMessage response)
            {
                using (response)
                {
                    if (response.IsSuccessStatusCode && await this._serializationService.DeserializeAsync<T>(response, ct) is T collection)
                    {
                        return collection;
                    }
                }
            }

            return default;
        }
    }
}
