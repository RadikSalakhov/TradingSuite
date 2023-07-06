namespace TaApi.Worker.Configuration
{
    public class TaApiOptions
    {
        public string TaApiUrl { get; set; } = string.Empty;

        public string TaApiKey { get; set; } = string.Empty;

        public int TaApiPeriodMS { get; set; } = 1000;

        public int TaApiSymbolsPerRequest { get; set; } = 1;
    }
}