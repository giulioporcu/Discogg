using Application.Extensions;
using MudBlazor;
using MudBlazor.Utilities;

namespace Application.Services
{
    /// <summary>
    /// Manages theme state, including dark mode and primary color, with persistence through local storage.
    /// </summary>
    public class ThemeService(LocalStorageService localStorage)
    {
        /// <summary>
        /// The local storage key used to store the active theme mode.
        /// </summary>
        public const string DATA_THEME_KEY = "data-theme";

        /// <summary>
        /// The local storage key used to store the primary color value.
        /// </summary>
        public const string PRIMARY_COLOR_KEY = "primary-color";

        /// <summary>
        /// The stored value representing dark mode.
        /// </summary>
        public const string DARK_THEME_KEY = "dark";

        /// <summary>
        /// The stored value representing light mode.
        /// </summary>
        public const string LIGHT_THEME_KEY = "light";

        /// <summary>
        /// The default primary color used when no value is stored.
        /// </summary>
        public const string DEFAULT_PRIMARY_COLOR_VALUE = "rgba(15,91,3,1)";

        /// <summary>
        /// Applies derived palette colors based on the provided base color.
        /// </summary>
        /// <param name="baseColor">The base color used to generate derived palette values.</param>
        private void ApplyDerivedColors(MudColor baseColor)
        {
            Palette light = this.CurrentTheme.PaletteLight;
            Palette dark = this.CurrentTheme.PaletteDark;

            if (baseColor?.ToRgba() is string baseRgba)
            {
                light.Primary = baseRgba;
                light.PrimaryLighten = baseColor.ColorLighten(0.20f).ToRgba()!;
                light.PrimaryDarken = baseColor.ColorDarken(0.20f).ToRgba()!;

                light.Secondary = baseColor.ColorLighten(0.15f);
                light.Info = baseColor.ColorLighten(0.25f);
                light.Success = baseColor.ColorDarken(0.15f);
                light.Warning = baseColor.ColorLighten(0.10f);
                light.Error = baseColor.ColorDarken(0.30f);

                light.AppbarBackground = baseColor.ColorDarken(0.40f);
                light.DrawerBackground = baseColor.ColorDarken(0.50f);

                dark.Primary = baseRgba;
                dark.PrimaryLighten = baseColor.ColorLighten(0.25f).ToRgba()!;
                dark.PrimaryDarken = baseColor.ColorDarken(0.25f).ToRgba()!;

                dark.Secondary = baseColor.ColorLighten(0.20f);
                dark.Info = baseColor.ColorLighten(0.30f);
                dark.Success = baseColor.ColorDarken(0.10f);
                dark.Warning = baseColor.ColorLighten(0.15f);
                dark.Error = baseColor.ColorDarken(0.35f);

                dark.AppbarBackground = baseColor.ColorDarken(0.60f);
                dark.DrawerBackground = baseColor.ColorDarken(0.70f);
            }
        }

        /// <summary>
        /// Gets or sets the primary color used by the theme.
        /// </summary>
        public string PrimaryColor
        {
            get => this.CurrentTheme.PaletteLight.Primary.ToString();
            set
            {
                MudColor? mudColor = MudColor.TryParse(value, out MudColor? parsed) && parsed != null
                    ? parsed
                    : MudColor.Parse(DEFAULT_PRIMARY_COLOR_VALUE);

                this.ApplyDerivedColors(mudColor);
                this.NotifyStateChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether dark mode is active.
        /// </summary>
        public bool IsDarkMode
        {
            get;
            set
            {
                field = value;
                this.NotifyStateChanged();
            }
        }

        /// <summary>
        /// Raised whenever theme or color settings change.
        /// </summary>
        public event Action? OnThemeChange;

        /// <summary>
        /// The currently active MudBlazor theme instance.
        /// </summary>
        public MudTheme CurrentTheme { get; private set; } = new()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#09F0AA",
                Secondary = "#4C82FB",
                Tertiary = "#FFB74D",
                Background = "#F7F9FC",
                Surface = "#FFFFFF",
                DrawerBackground = "#FFFFFF",
                DrawerText = "#2A2A2A",
                AppbarBackground = "#FFFFFF",
                AppbarText = "#2A2A2A",
                TextPrimary = "#1F1F1F",
                TextSecondary = "#555555",
                ActionDefault = "#6E6E6E",
                ActionDisabled = "#BDBDBD",
                ActionDisabledBackground = "#E0E0E0",
                Divider = "#E5E7EB",
                DividerLight = "#F0F2F5",
                TableLines = "#E5E7EB",
                LinesDefault = "#E5E7EB",
                LinesInputs = "#D1D5DB",
                Success = "#4CAF50",
                Warning = "#FB8C00",
                Error = "#E53935",
                Info = "#1E88E5"
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#09F0AA",
                Secondary = "#4C82FB",
                Tertiary = "#FFFFB74D",
                Background = "#121212",
                Surface = "#1E1E1E",
                DrawerBackground = "#1C1C1C",
                DrawerText = "#E0E0E0",
                AppbarBackground = "#1C1C1C",
                AppbarText = "#FFFFFF",
                TextPrimary = "#FFFFFF",
                TextSecondary = "#BDBDBD",
                ActionDefault = "#9E9E9E",
                ActionDisabled = "#555555",
                ActionDisabledBackground = "#2A2A2A",
                Divider = "#2A2A2A",
                DividerLight = "#333333",
                TableLines = "#2A2A2A",
                LinesDefault = "#2A2A2A",
                LinesInputs = "#3A3A3A",
                Success = "#66BB6A",
                Warning = "#FFA726",
                Error = "#EF5350",
                Info = "#42A5F5"
            }
        };

        /// <summary>
        /// Loads theme settings from local storage.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InitializeAsync()
        {
            this.IsDarkMode = await localStorage.GetAsync(DATA_THEME_KEY) == DARK_THEME_KEY;
            this.PrimaryColor = await localStorage.GetAsync(PRIMARY_COLOR_KEY) ?? DEFAULT_PRIMARY_COLOR_VALUE;
        }

        /// <summary>
        /// Persists theme settings to local storage.
        /// </summary>
        public void Persist() => this.PersistAsync().FireAndForget();

        /// <summary>
        /// Persists theme settings to local storage.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async ValueTask PersistAsync()
        {
            await localStorage.SetAsync(PRIMARY_COLOR_KEY, this.PrimaryColor);
            await localStorage.SetAsync(DATA_THEME_KEY, this.IsDarkMode ? DARK_THEME_KEY : LIGHT_THEME_KEY);
        }

        /// <summary>
        /// Notifies subscribers that theme state has changed.
        /// </summary>
        private void NotifyStateChanged() => this.OnThemeChange?.Invoke();
    }
}