using System.Reflection;

namespace Discogs.API.Framework.Utility
{
    public static class CurrentAssemblyInfo
    {
        private static Assembly ExecutingAssembly { get; } = Assembly.GetExecutingAssembly();

        public static string GetVersion()
        {
            if (ExecutingAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>() is AssemblyInformationalVersionAttribute attribute)
            {
                string raw = attribute.InformationalVersion;

                if (String.IsNullOrWhiteSpace(raw))
                {
                    throw new ArgumentException("Informational version is empty.");
                }

                string numericPart = raw.Split('-', '+')[0];

                return Version.TryParse(numericPart, out Version? version)
                    ? $"{version.Major}.{version.Minor}"
                    : throw new ArgumentException("Informational version is not parsable.");
            }
            else
            {
                throw new ArgumentException("The version info is not set.");
            }
        }

        public static string GetProduct()
        {
            return ExecutingAssembly.GetCustomAttribute<AssemblyProductAttribute>() is AssemblyProductAttribute attribute
                ? attribute.Product
                : throw new ArgumentException("The product info is not set.");
        }

        public static string GetCopyright()
        {
            return ExecutingAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>() is AssemblyCopyrightAttribute attribute
                ? attribute.Copyright
                : throw new ArgumentException("The copyright info is not set.");
        }
    }
}
