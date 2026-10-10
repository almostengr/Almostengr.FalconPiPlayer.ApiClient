using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.FalconPiPlayer.ApiClient.Fppd.DomainServices.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Fppd.DomainServices.Resources;
using Almostengr.LightShow.Agent.Services.AppSettingsManager.Domain;
using Almostengr.LightShow.Agent.Services.CurrentStatusManager.Domain;
using Almostengr.LightShow.Common;
using Almostengr.LightShow.Web.ApiClient;
using Microsoft.Extensions.Options;

namespace Almostengr.LightShow.Agent.Services;

public sealed class AgentWorker : BackgroundService
{
    private readonly IFppdClient _fppdClient;
    private readonly ILogger<AgentWorker> _logger;
    private readonly IStatusHistoryClient _statusHistoryClient;
    private AppSettings.AgentSettings _agentSettings;
    private StatusHistoryResource _lastResource = null;

    public AgentWorker(
        IFppdClient fppdClient,
        IStatusHistoryClient statusHistoryClient,
        ILogger<AgentWorker> logger,
        IOptionsMonitor<AppSettings.AgentSettings> options
    )
    {
        _fppdClient = fppdClient;
        _logger = logger;
        options.OnChange(agentsettings =>
        {
            _agentSettings = agentsettings;
        });
        _statusHistoryClient = statusHistoryClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!RunMode.IsOnline)
            {
                await DelayAsync(stoppingToken);
                continue;
            }

            try
            {
                FppdStatusResource fppStatus = await _fppdClient.GetStatusAsync();

                if (fppStatus == null)
                {
                    await DelayAsync(stoppingToken);
                    continue;
                }

                var currentResource = AssignToResource(fppStatus);
                if (_lastResource == currentResource)
                {
                    await DelayAsync(stoppingToken);
                    continue;
                }

                var statusResult = await _statusHistoryClient.CreateAsync(currentResource);
                if (statusResult.Failed)
                {
                    _logger.LogWarning(statusResult.ToErrorString());
                }

                var requestResult = await GetNextRequestAsync();
                if (requestResult.Failed)
                {
                    _logger.LogWarning(requestResult.ToErrorString());
                }

                _lastResource = currentResource;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            await DelayAsync(stoppingToken);
        }
    }

    private async Task<Result<int>> GetNextRequestAsync()
    {
        if (!RunMode.AllowRequests)
        {
            return Result<int>.Failure(string.Empty);
        }

        throw new NotImplementedException();
    }

    private static StatusHistoryResource AssignToResource(FppdStatusResource statusResource)
    {
        if (statusResource == null)
        {
            return null;
        }

        return new StatusHistoryResource
        {
            CurrentSequence = statusResource.CurrentSequence,
            CurrentSong = statusResource.CurrentSong,
            AllowRequests = RunMode.AllowRequests,
            IsOnline = RunMode.IsOnline,
        };
    }

    private async Task DelayAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(_agentSettings.WorkerSleepInterval), cancellationToken);
    }
}
