using Almostengr.Common.Common.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Common.DomainServices.Resources;
using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;
using Almostengr.FalconPiPlayer.ApiClient.Media.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Playlist.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Scripts.DomainService.Interfaces;
using Microsoft.Extensions.Options;

namespace Almostengr.FalconPiPlayer.ApiClient.Playlist.Infrastructure;

public class PlaylistClient : BaseFppClient, IPlaylistClient
{
    public PlaylistClient(
        HttpClient httpClient,
        IOptions<FppApiClientSettings> options
        ) : base(httpClient, options)
    {
    }

    public async Task<FppStatusMessageResource> DeleteByNameAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        string route = $"api/playlist/{name}";
        var response = await _httpClient.GetAsync<FppStatusMessageResource>(route);
        return response;
    }

    public async Task<PlaylistResource> GetByNameAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        string route = $"api/playlist/{name}";
        var response = await _httpClient.GetAsync<PlaylistResource>(route);
        return response;
    }

    public async Task<IEnumerable<string>> GetListAsync()
    {
        string route = "api/playlist";
        var response = await _httpClient.GetAsync<IEnumerable<string>>(route);
        return response;
    }

    public async Task<IEnumerable<string>> GetPlayableAsync()
    {
        string route = "api/playlist/playable";
        var response = await _httpClient.GetAsync<IEnumerable<string>>(route);
        return response;
    }

    public async Task<string> PauseAsync()
    {
        string route = "api/playlist/pause";
        var response = await _httpClient.GetAsync<string>(route);
        return response;
    }

    public async Task<string> ResumeAsync()
    {
        string route = "api/playlist/resume";
        var response = await _httpClient.GetAsync<string>(route);
        return response;
    }

    public async Task<string> StartByNameAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        string route = $"api/playlist/{name}/start";
        var response = await _httpClient.GetAsync<string>(route);
        return response;
    }

    public async Task<string> StopAsync()
    {
        string route = "api/playlist/stop";
        var response = await _httpClient.GetAsync<string>(route);
        return response;
    }

    public async Task<string> StopGracefullyAfterLoopAsync()
    {
        string route = "api/playlist/stopgracefullyafterloop";
        var response = await _httpClient.GetAsync<string>(route);
        return response;
    }

    public async Task<string> StopGracefullyAsync()
    {
        string route = "api/playlist/stopgracefully";
        var response = await _httpClient.GetAsync<string>(route);
        return response;
    }

    public async Task<FppStatusMessageResource> UpsertByNameAsync(PlaylistResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));
        ArgumentException.ThrowIfNullOrWhiteSpace(resource.Name, nameof(resource.Name));

        string route = $"api/playlist/{resource.Name}";
        var response = await _httpClient.PostAsync<PlaylistResource, FppStatusMessageResource>(route, resource);
        return response;
    }
}
