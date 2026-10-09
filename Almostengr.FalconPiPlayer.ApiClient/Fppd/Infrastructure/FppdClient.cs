using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;
using Almostengr.FalconPiPlayer.ApiClient.Fppd.DomainServices.Resources;
using Almostengr.FalconPiPlayer.ApiClient.Fppd.DomainServices.Interfaces;
using Microsoft.Extensions.Options;
using Almostengr.Common.Common.Infrastructure;

namespace Almostengr.FalconPiPlayer.ApiClient.Fppd.Infrastructure;

public sealed class FppdClient : IFppdClient
{
    private readonly FppApiClientSettings _settings;
    private readonly HttpClient _httpClient;

    public FppdClient(
        IOptions<FppApiClientSettings> options,
        HttpClient httpClient
        )
    {
        _settings = options.Value;

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_settings.HostUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
    }

    public async Task<FppdStatusResource> GetStatusAsync()
    {
        string route = "api/fppd/status";
        FppdStatusResource response = await _httpClient.GetAsync<FppdStatusResource>(route);
        return response;
    }

    public async Task<FppdVolumeResource> GetVolumeAsync()
    {
        string route = $"api/fppd/volume";
        FppdVolumeResource response = await _httpClient.GetAsync<FppdVolumeResource>(route);
        return response;
    }

    public async Task<FppdE131StatsResource> GetFppdE131StatsAsync()
    {
        string route = "api/fppd/e131stats";
        FppdE131StatsResource response = await _httpClient.GetAsync<FppdE131StatsResource>(route);
        return response;
    }

    public async Task<FppdEffectsResource> GetEffectsResourceAsync()
    {
        string route = "api/fppd/effects";
        FppdEffectsResource response = await _httpClient.GetAsync<FppdEffectsResource>(route);
        return response;
    }

    public async Task<FppdLogResource> GetLogResourceAsync()
    {
        string route = "api/fppd/log";
        FppdLogResource response = await _httpClient.GetAsync<FppdLogResource>(route);
        return response;
    }

    public async Task<FppdMultiSyncStatsResource> GetFppdMultiSyncStatsAsync()
    {
        string route = "api/fppd/multiSyncStats";
        FppdMultiSyncStatsResource response = await _httpClient.GetAsync<FppdMultiSyncStatsResource>(route);
        return response;
    }

    public async Task<FppdMultiSyncSystemsResource> GetFppdMultiSyncSystemsAsync()
    {
        string route = "api/fppd/multiSyncSystems";
        FppdMultiSyncSystemsResource response = await _httpClient.GetAsync<FppdMultiSyncSystemsResource>(route);
        return response;
    }

    public async Task<FppdScheduleResource> GetFppdScheduleAsync()
    {
        string route = "api/fppd/schedule";
        FppdScheduleResource response = await _httpClient.GetAsync<FppdScheduleResource>(route);
        return response;
    }

    public async Task<FppdTestingResource> GetFppdTestingAsync()
    {
        string route = "api/fppd/testing";
        FppdTestingResource response = await _httpClient.GetAsync<FppdTestingResource>(route);
        return response;
    }

    public async Task<FppdVersionResource> GetFppdVersionAsync()
    {
        string route = "api/fppd/version";
        FppdVersionResource response = await _httpClient.GetAsync<FppdVersionResource>(route);
        return response;
    }
}
