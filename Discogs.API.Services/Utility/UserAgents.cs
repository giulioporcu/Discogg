using System.Diagnostics;

namespace Discogs.API.Framework.Utility
{
    /// <summary>
    /// Generates user-agent strings based on installed browsers.
    /// </summary>
    public static class UserAgents
    {
        /// <summary>
        /// Generates a user-agent string based on a locally installed browser.
        /// </summary>
        /// <returns>A user-agent string.</returns>
        public static readonly Lazy<string> Default = new(() =>
        {
            string? windowsVersion = GetWindowsVersion();

            string? chromeVersion = GetExecutableVersion(@"C:\Program Files\Google\Chrome\Application\chrome.exe");
            string? edgeVersion = GetExecutableVersion(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe");
            string? firefoxVersion = GetExecutableVersion(@"C:\Program Files\Mozilla Firefox\firefox.exe");

            if (!String.IsNullOrWhiteSpace(chromeVersion))
            {
                return $"Mozilla/5.0 ({windowsVersion}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{chromeVersion} Safari/537.36";
            }

            if (!String.IsNullOrWhiteSpace(edgeVersion))
            {
                return $"Mozilla/5.0 ({windowsVersion}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{edgeVersion} Safari/537.36 Edg/{edgeVersion}";
            }

            if (!String.IsNullOrWhiteSpace(firefoxVersion))
            {
                string major = firefoxVersion.Split('.')[0];
                return $"Mozilla/5.0 ({windowsVersion}; Win64; x64; rv:{major}) Gecko/20100101 Firefox/{firefoxVersion}";
            }

            // Fallback
            return $"Mozilla/5.0 ({windowsVersion}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36";
        });

        /// <summary>
        /// Gets the Windows NT version string.
        /// </summary>
        /// <returns>A string in the format "Windows NT major.minor".</returns>
        private static string GetWindowsVersion()
        {
            Version version = Environment.OSVersion.Version;
            return $"Windows NT {version.Major}.{version.Minor}";
        }

        /// <summary>
        /// Gets the file version of an executable at the specified path.
        /// </summary>
        /// <param name="executablePath">The full path to the executable.</param>
        /// <returns>The file version, or null if the file does not exist.</returns>
        private static string? GetExecutableVersion(string executablePath)
            => File.Exists(executablePath) ? FileVersionInfo.GetVersionInfo(executablePath).FileVersion : null;
    }
}