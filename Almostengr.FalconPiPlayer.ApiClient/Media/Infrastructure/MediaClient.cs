using Almostengr.Common.Common.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;
using Almostengr.FalconPiPlayer.ApiClient.Media.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Media.DomainService.Resources;
using Microsoft.Extensions.Options;

namespace Almostengr.FalconPiPlayer.ApiClient.Media.Infrastructure;

public sealed class MediaClient : IMediaClient
{
    private readonly FppApiClientSettings _settings;
    private readonly HttpClient _httpClient;

    public MediaClient(
        IOptions<FppApiClientSettings> options,
        HttpClient httpClient
        )
    {
        _settings = options.Value;

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_settings.HostUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
    }

    public async Task<IEnumerable<string>> GetListAsync()
    {
        string route = "api/media";
        var response = await _httpClient.GetAsync<IEnumerable<string>>(route);
        return response;
    }

    public async Task<MediaMetaResource> GetMetaAsync(MediaNameResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        string route = $"api/media/{resource.Name}/meta";
        MediaMetaResource response = await _httpClient.GetAsync<MediaMetaResource>(route);
        return response;
    }
}
