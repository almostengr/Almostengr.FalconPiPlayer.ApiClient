using System.Text.Json;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.LightShow.Agent.Services.AppSettingsManager.Domain;
using Microsoft.Extensions.Options;

namespace Almostengr.LightShow.Agent.Services.AppSettingsManager;

public sealed class UpdateAppSettingsService : IUpdateService<AppSettingsResource>
{
    private readonly IOptionsSnapshot<AppSettings> _options;

    public UpdateAppSettingsService(
        IOptionsSnapshot<AppSettings> options
    )
    {
        _options = options;
    }

    public async Task<Result<AppSettingsResource>> ExecuteAsync(AppSettingsResource resource, bool commitTransaction = true)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            AppSettings appSettings = _options.Value;
            appSettings.Agent.PlayerTypeId = (int)resource.TypeOption;
            appSettings.Agent.PlayerUrl = resource.PlayerUrl;
            appSettings.Agent.WebsiteUrl = resource.WebsiteUrl;
            appSettings.Agent.WebsiteApiKey = resource.WebsiteApiKey;
            appSettings.Agent.WorkerSleepInterval = resource.WorkerSleepInterval;

            var json = JsonSerializer.Serialize(appSettings);

            await File.WriteAllTextAsync("appsettings.json", json);

            return Result<AppSettingsResource>.Success(resource);
        }
        catch (Exception ex)
        {
            return Result<AppSettingsResource>.Failure(ex.Message);
        }
    }
}
