using Almostengr.LightShow.Agent.Services.CurrentStatusManager.Domain;

namespace Almostengr.LightShow.Agent.Services.CurrentStatusManager;

public static class CurrentStatusExtensions
{
    public static void AddCurrentStatusServices(this IServiceCollection services)
    {
        services.AddTransient<IUpdateCurrentStatusService, UpdateCurrentStatusService>();
    }
}