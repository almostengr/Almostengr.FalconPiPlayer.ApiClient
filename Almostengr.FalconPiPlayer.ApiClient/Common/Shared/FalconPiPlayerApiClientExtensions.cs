using Almostengr.FalconPiPlayer.ApiClient.Command.DomainServices.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Command.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Fppd.DomainServices.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Fppd.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Media.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Media.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Playlist.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Playlist.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.Scripts.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.Scripts.Infrastructure;
using Almostengr.FalconPiPlayer.ApiClient.System.DomainService.Interfaces;
using Almostengr.FalconPiPlayer.ApiClient.System.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Almostengr.FalconPiPlayer.ApiClient.Common.Shared;

public static class FalconPiPlayerApiClientExtensions
{
    public static void AddFalconPiPlayerApiClientServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddHttpClient();
        // +            services.Configure<FppApiClientSettings>()(nameof(FppApiClientSettings))
        services.AddTransient<ICommandClient, CommandClient>();
        services.AddTransient<IFppdClient, FppdClient>();
        services.AddTransient<IMediaClient, MediaClient>();
        services.AddTransient<IPlaylistClient, PlaylistClient>();
        services.AddTransient<IScriptsClient, ScriptsClient>();
        services.AddTransient<ISystemClient, SystemClient>();
    }
}