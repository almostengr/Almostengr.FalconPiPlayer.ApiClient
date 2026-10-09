using Almostengr.Common.Common.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Common.DomainServices.Resources;
using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;
using Almostengr.FalconPiPlayer.ApiClient.System.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.System.DomainService.Resources;
using Microsoft.Extensions.Options;

namespace Almostengr.FalconPiPlayer.ApiClient.System.Infrastructure;

public class SystemClient : ISystemClient
{
    private readonly FppApiClientSettings _settings;
    private readonly HttpClient _httpClient;

    public SystemClient(
        IOptions<FppApiClientSettings> options,
        HttpClient httpClient
        )
    {
        _settings = options.Value;

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_settings.HostUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
    }

    public async Task<SystemInfoResource> GetSystemInfoAsync()
    {
        string route = "api/system/info";
        SystemInfoResource response = await _httpClient.GetAsync<SystemInfoResource>(route);
        return response;
    }

    public async Task<SystemStatusResource> GetSystemStatusAsync()
    {
        string route = "api/system/status";
        SystemStatusResource response = await _httpClient.GetAsync<SystemStatusResource>(route);
        return response;
    }

    public async Task<SystemVolumeResource> GetSystemVolumeAsync()
    {
        string route = "api/system/volume";
        SystemVolumeResource response = await _httpClient.GetAsync<SystemVolumeResource>(route);
        return response;
    }

    public async Task<FppStatusResource> RebootSystemAsync()
    {
        string route = "api/system/reboot";
        FppStatusResource response = await _httpClient.GetAsync<FppStatusResource>(route);
        return response;
    }

    public async Task<FppStatusResource> RestartFppdAsync()
    {
        string route = "api/system/fppd/restart";
        FppStatusResource response = await _httpClient.GetAsync<FppStatusResource>(route);
        return response;
    }

    public async Task<FppStatusResource> ShutdownSystemAsync()
    {
        string route = "api/system/shutdown";
        FppStatusResource response = await _httpClient.GetAsync<FppStatusResource>(route);
        return response;
    }

    public async Task<FppStatusResource> StartFppdAsync()
    {
        string route = "api/system/fppd/start";
        FppStatusResource response = await _httpClient.GetAsync<FppStatusResource>(route);
        return response;
    }

    public async Task<FppStatusResource> StopFppdAsync()
    {
        string route = "api/system/fppd/stop";
        FppStatusResource response = await _httpClient.GetAsync<FppStatusResource>(route);
        return response;
    }

    public async Task<FppStatusResource> UpdateSystemVolumeAsync(SystemVolumeResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        string route = "api/system/volume";
        FppStatusResource response = await _httpClient.PostAsync<SystemVolumeResource, FppStatusResource>(route, resource);
        return response;
    }
}