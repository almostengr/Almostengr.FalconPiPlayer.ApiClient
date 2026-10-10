using Almostengr.LightShow.Agent.Models.Entities;
using Almostengr.LightShow.Agent.Services.Profiles;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;

namespace Almostengr.LightShow.Agent.Services;

public sealed class AgentWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AgentWorker(
        HttpClient httpClient,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string lastSequence = string.Empty;

        while (!stoppingToken.IsCancellationRequested)
        {
            var profile = await GetActiveProfileAsync();
            if (profile == null)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                continue;
            }

            if (!StatusHistory.IsOnline)
            {
                await Task.Delay(TimeSpan.FromSeconds(profile.WorkerSleepInterval), stoppingToken);
                continue;
            }

            // get latest fpp status 

            // if last sequence / song is not the same


            await Task.Delay(TimeSpan.FromSeconds(profile.WorkerSleepInterval), stoppingToken);
        }
    }

    private async Task<Profile> GetActiveProfileAsync()
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var queryService = scope.ServiceProvider
            .GetRequiredService<IQueryProfileService>();

        var profile = await queryService.GetActiveAsync();
        return profile;
    }
}
