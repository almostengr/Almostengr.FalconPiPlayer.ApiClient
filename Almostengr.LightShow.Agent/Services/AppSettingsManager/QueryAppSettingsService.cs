using Almostengr.LightShow.Agent.Services.AppSettingsManager.Domain;
using Microsoft.Extensions.Options;

namespace Almostengr.LightShow.Agent.Services.AppSettingsManager;

public sealed class QueryAppSettingsService : IQueryAppSettingsService
{
    private readonly IOptionsSnapshot<AppSettings> _options;

    public QueryAppSettingsService(
        IOptionsSnapshot<AppSettings> options
    )
    {
        _options = options;
    }

    public AppSettingsResource Get()
    {
        return new AppSettingsResource
        {
            PlayerUrl = _options.Value.Agent.PlayerUrl,
            WebsiteUrl = _options.Value.Agent.WebsiteUrl,
            WebsiteApiKey = _options.Value.Agent.WebsiteApiKey,
            WorkerSleepInterval = _options.Value.Agent.WorkerSleepInterval,
            TypeOption = _options.Value.Agent.TypeOption
        };
    }
}
