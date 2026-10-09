using System.Text;
using Almostengr.Common.Common.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Command.DomainServices.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Command.DomainServices.Resources;
using Almostengr.FalconPiPlayer.ApiClient.Common.Shared;
using Microsoft.Extensions.Options;

namespace Almostengr.FalconPiPlayer.ApiClient.Command.Infrastructure;

public class CommandClient : ICommandClient
{
    private readonly FppApiClientSettings _settings;
    private readonly HttpClient _httpClient;

    public CommandClient(
        IOptions<FppApiClientSettings> options,
        HttpClient httpClient
    )
    {
        _settings = options.Value;

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_settings.HostUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
    }

    public async Task<string> GetCommandAsync(CommandResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        StringBuilder route = new();
        route.Append("api/command/");
        route.Append(resource.Command);
        foreach (string argument in resource.Arguments)
        {
            route.Append('/');
            route.Append(argument);
        }

        string response = await _httpClient.GetAsync<string>(route.ToString());
        return response;
    }

    public async Task<string> PostCommandAsync(CommandResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        string route = $"api/command/{resource.Command}";
        string response = await _httpClient.PostAsync<CommandResource, string>(route, resource);
        return response;
    }
}
