using System.Reflection;

namespace Discogs.API.Framework.Utility
{
    /// <summary>
    /// Provides metadata from the current assembly.
    /// </summary>
    public static class CurrentAssemblyInfo
    {
        /// <summary>
        /// Gets the currently executing assembly.
        /// </summary>
        private static Assembly ExecutingAssembly { get; } = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Gets the assembly's informational version.
        /// </summary>
        /// <exception cref="InvalidOperationException">The version attribute is missing, empty, or unparsable.</exception>
        public static readonly Lazy<string> Version = new(() =>
        {
            if (ExecutingAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>() is AssemblyInformationalVersionAttribute attribute)
            {
                string raw = attribute.InformationalVersion;

                if (String.IsNullOrWhiteSpace(raw))
                {
                    throw new InvalidOperationException("Informational version is empty.");
                }

                string numericPart = raw.Split('-', '+')[0];

                return System.Version.TryParse(numericPart, out Version? version)
                        ? $"{version.Major}.{version.Minor}"
                        : throw new InvalidOperationException("Informational version is not parsable.");
            }
            else
            {
                throw new InvalidOperationException("The version info is not set.");
            }
        });

        /// <summary>
        /// Gets the assembly product name.
        /// </summary>
        /// <exception cref="InvalidOperationException">The product attribute is not set.</exception>
        public static readonly Lazy<string> Product = new(() =>
        {
            return ExecutingAssembly.GetCustomAttribute<AssemblyProductAttribute>() is AssemblyProductAttribute attribute
                ? attribute.Product
                : throw new InvalidOperationException("The product info is not set.");
        });

        /// <summary>
        /// Gets the assembly copyright information.
        /// </summary>
        /// <exception cref="InvalidOperationException">The copyright attribute is not set.</exception>
        public static readonly Lazy<string> Copyright = new(() =>
        {
            return ExecutingAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>() is AssemblyCopyrightAttribute attribute
                ? attribute.Copyright
                : throw new InvalidOperationException("The copyright info is not set.");
        });
    }
}