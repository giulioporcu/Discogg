namespace Discogs.API.Framework.Extensions
{
    /// <summary>
    /// Provides extension methods for string manipulation.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Ensures the string starts with the specified prefix.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="prefix">The prefix to prepend if not already present.</param>
        /// <returns>The string with the prefix prepended.</returns>
        public static string EnsureStartsWith(this string value, string prefix)
            => value is null || value.StartsWith(prefix, StringComparison.Ordinal) ? value ?? prefix : prefix + value;
    }
}