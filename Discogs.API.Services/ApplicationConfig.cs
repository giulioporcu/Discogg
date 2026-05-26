using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discogs.API.Services
{
    public sealed class ApplicationConfig
    {
        [JsonPropertyName("base_url")]
        public string BaseUrl
        {
            get;
            private set => field = String.IsNullOrEmpty(value) ? String.Empty : value;
        } = String.Empty;

        [JsonPropertyName("max_retries")]
        public int MaxRetries
        {
            get;
            private set => field = value < 0 ? 0 : value;
        } = 0;

        [JsonPropertyName("initial_delay_ms")]
        public int InitialDelayMs
        {
            get;
            private set => field = value < 0 ? 0 : value;
        } = 0;

        public ApplicationConfig()
        {
            try
            {
                string configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
                if (!File.Exists(configPath))
                {
                    configPath = "config.json";
                }

                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    ApplicationConfig? config = JsonSerializer.Deserialize<ApplicationConfig>(json);

                    if (config is not null)
                    {
                        this.BaseUrl = config.BaseUrl;
                        this.MaxRetries = config.MaxRetries;
                        this.InitialDelayMs = config.InitialDelayMs;
                    }
                }
            }
            catch
            {
                // defaults
            }
        }
    }
}
