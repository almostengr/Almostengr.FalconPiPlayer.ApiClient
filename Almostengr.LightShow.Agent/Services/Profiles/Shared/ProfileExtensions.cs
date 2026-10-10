using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;

namespace Almostengr.LightShow.Agent.Services.Profiles.Shared;

public static class ProfileExtensions
{
    public static void AddProfileServices(this IServiceCollection services)
    {
        services.AddTransient<IAddService<ProfileResource>, AddProfileService>();
        services.AddTransient<IUpdateService<ProfileResource>, UpdateProfileService>();
        services.AddTransient<IQueryService<Profile, ProfileResource>, QueryProfileService>();
        services.AddTransient<IDeleteService<ProfileResource>, DeleteProfileService>();
    }
}