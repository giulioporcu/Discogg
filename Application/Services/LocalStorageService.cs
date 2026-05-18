using Microsoft.JSInterop;

namespace Application.Services
{
    /// <summary>
    /// Provides access to browser local storage through JavaScript interop.
    /// </summary>
    /// <remarks>
    /// Supports plain and encrypted storage operations using custom JavaScript functions.
    /// </remarks>
    public class LocalStorageService(IJSRuntime jsRuntime)
    {
        /// <summary>
        /// Stores a value in local storage.
        /// </summary>
        /// <param name="key">The storage key.</param>
        /// <param name="value">The value to store.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public ValueTask SetAsync(string key, string value)
            => jsRuntime.InvokeVoidAsync("discogg.localStorage.setAsync", key, value);

        /// <summary>
        /// Retrieves a value from local storage.
        /// </summary>
        /// <param name="key">The storage key.</param>
        /// <returns>The stored value, or null if not found.</returns>
        public ValueTask<string?> GetAsync(string key)
            => jsRuntime.InvokeAsync<string?>("discogg.localStorage.getAsync", key);

        /// <summary>
        /// Stores an encrypted value in local storage.
        /// </summary>
        /// <param name="key">The storage key.</param>
        /// <param name="value">The encrypted value to store.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public ValueTask SetEncryptedAsync(string key, string value)
            => jsRuntime.InvokeVoidAsync("discogg.localStorage.setEncryptedAsync", key, value);

        /// <summary>
        /// Retrieves an encrypted value from local storage.
        /// </summary>
        /// <param name="key">The storage key.</param>
        /// <returns>The decrypted value, or null if not found.</returns>
        public ValueTask<string?> GetDecryptedAsync(string key)
            => jsRuntime.InvokeAsync<string?>("discogg.localStorage.getDecryptedAsync", key);

        /// <summary>
        /// Removes the given key from local storage.
        /// </summary>
        /// <param name="key">The storage key.</param>
        public ValueTask<string?> RemoveAsync(string key)
            => jsRuntime.InvokeAsync<string?>("discogg.localStorage.removeAsync", key);
    }
}