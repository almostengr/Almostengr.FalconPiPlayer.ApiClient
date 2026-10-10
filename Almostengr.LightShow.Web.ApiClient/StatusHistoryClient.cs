using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.Common.Common.Infrastructure;
using Almostengr.LightShow.Common;
using Microsoft.Extensions.Options;

namespace Almostengr.LightShow.Web.ApiClient;

public class StatusHistoryClient : IStatusHistoryClient
{
    private readonly LightShowSettings _settings;
    private readonly HttpClient _httpClient;

    public StatusHistoryClient(
        IOptions<LightShowSettings> options,
        HttpClient httpClient
    )
    {
        _settings = options.Value;

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_settings.HostUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
    }

    public async Task<Result<StatusHistoryResource>> CreateAsync(StatusHistoryResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        string route = "api/statushistories";
        Result<StatusHistoryResource> response =
            await _httpClient.PostAsync<StatusHistoryResource, Result<StatusHistoryResource>>(route, resource);
        return response;
    }
}
