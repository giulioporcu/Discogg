using System.Diagnostics;

namespace Discogs.API.Framework.Utility
{
    public static class UserAgents
    {
        public static string Generate()
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
        }

        private static string GetWindowsVersion()
        {
            Version version = Environment.OSVersion.Version;
            return $"Windows NT {version.Major}.{version.Minor}";
        }

        private static string? GetExecutableVersion(string executablePath)
            => File.Exists(executablePath) ? FileVersionInfo.GetVersionInfo(executablePath).FileVersion : null;
    }
}