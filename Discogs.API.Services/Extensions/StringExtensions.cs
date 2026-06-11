namespace Discogs.API.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string EnsureStartsWith(this string value, string prefix)
            => value == null ? prefix : prefix + value.TrimStart(prefix.ToCharArray());
    }
}