using Almostengr.Common.Common.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Common.DomainServices.Resources;
using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;
using Almostengr.FalconPiPlayer.ApiClient.Scripts.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Scripts.DomainService.Resources;
using Microsoft.Extensions.Options;

namespace Almostengr.FalconPiPlayer.ApiClient.Scripts.Infrastructure;

public class ScriptsClient : BaseFppClient, IScriptsClient
{
    public ScriptsClient(
        HttpClient httpClient,
        IOptions<FppApiClientSettings> options
    ) : base(httpClient, options)
    {

    }
    public async Task<ScriptBodyResource> GetByNameAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        string route = $"api/scripts/{name}";
        var response = await _httpClient.GetAsync<ScriptBodyResource>(route);
        return response;
    }

    public async Task<IEnumerable<string>> GetListAsync()
    {
        string route = "api/scripts";
        var response = await _httpClient.GetAsync<IEnumerable<string>>(route);
        return response;
    }

    public async Task<FppStatusResource> InstallRemoteAsync(RemoteScriptResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        string route = $"api/scripts/installremote/{resource.Category}/{resource.FileName}";
        var response = await _httpClient.GetAsync<FppStatusResource>(route);
        return response;
    }

    public async Task<string> RunByNameAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        string route = $"api/scripts/{name}/run";
        var response = await _httpClient.GetStringAsync(route);
        return response;
    }

    public async Task<string> ViewRemoteAsync(RemoteScriptResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        string route = $"api/scripts/viewremote/{resource.Category}/{resource.FileName}";
        var response = await _httpClient.GetStringAsync(route);
        return response;
    }
}
