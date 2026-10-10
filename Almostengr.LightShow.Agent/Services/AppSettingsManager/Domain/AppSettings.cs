using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;

namespace Almostengr.LightShow.Agent.Services.AppSettingsManager.Domain;

public class AppSettings
{
    public LoggingSettings Logging { get; set; } = new();
    public string AllowedHosts { get; set; } = "*";
    public FppApiClientSettings FppApiClientSettings { get; set; } = new();
    public AgentSettings Agent { get; set; } = new();

    public class AgentSettings
    {
        public int PlayerTypeId { get; set; }
        public string PlayerUrl { get; set; } = "http://localhost";
        public string WebsiteUrl { get; set; } = "http://lightshow.com";
        public string WebsiteApiKey { get; set; } = string.Empty;
        public PlayerTypeOption TypeOption => (PlayerTypeOption)PlayerTypeId;

        [Range(1, int.MaxValue, ErrorMessage = "Sleep interval must be greater than zero.")]
        public int WorkerSleepInterval { get; set; }
    }

    public sealed class LoggingSettings
    {
        public LogLevelSettings LogLevel { get; set; } = new();
    }

    public sealed class LogLevelSettings
    {
        public string Default { get; set; } = "Information";

        [JsonPropertyName("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }
    }
}