using Discogs.API.Core;
using Discogs.API.Framework.Utility;

namespace Discogs.API.Framework.Services
{
    public class VersionInfoService(DiscogsClient client, JsonSerializationService serializationService)
    {
        private DiscogsClient Client { get; } = client;
        private JsonSerializationService JsonSerializationService { get; } = serializationService;

        public string GetApplicationVersion()
        {
            try
            {
                return CurrentAssemblyInfo.GetVersion();
            }
            catch (Exception exception)
            {
                this.Client.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }

        public async Task<ApiVersionInfo> GetDiscogsVersionAsync(CancellationToken ct = default)
        {
            try
            {
                using HttpResponseMessage responseMessage = await this.Client.DoGetRequestAsync(path: null, ct);

                return await this.JsonSerializationService.DeserializeContentAsync<ApiVersionInfo>(responseMessage, ct)
                    ?? throw new Exception("Unable to parse Discogs response.");
            }
            catch (Exception exception)
            {
                this.Client.TriggerErrorEvent(this, exception.Message);
                throw;
            }
        }
    }
}