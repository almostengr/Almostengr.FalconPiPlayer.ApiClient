using Almostengr.LightShow.Agent.Services.AppSettingsManager;
using Almostengr.LightShow.Agent.Services.AppSettingsManager.Domain;

namespace Almostengr.LightShow.Agent.Models;

public class AppSettingsViewModel
{
    public AppSettingsViewModel()
    {
    }

    public AppSettingsViewModel(AppSettingsResource resource)
    {
        TypeOption = resource.TypeOption;
        PlayerUrl = resource.PlayerUrl;
        WebsiteUrl = resource.WebsiteUrl;
        WebsiteApiKey = resource.WebsiteApiKey;
        WorkerSleepInterval = resource.WorkerSleepInterval;
    }

    public PlayerTypeOption TypeOption { get; set; }
    public string PlayerUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public string WebsiteApiKey { get; set; }
    public int WorkerSleepInterval { get; set; }

    internal void AssignToResource(AppSettingsResource resource)
    {
        if (resource == null)
        {
            return;
        }

        resource.TypeOption = TypeOption;
        resource.PlayerUrl = PlayerUrl;
        resource.WebsiteUrl = WebsiteUrl;
        resource.WebsiteApiKey = WebsiteApiKey;
        resource.WorkerSleepInterval = WorkerSleepInterval;
    }
}