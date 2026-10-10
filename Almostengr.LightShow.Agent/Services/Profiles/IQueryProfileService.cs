using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;

namespace Almostengr.LightShow.Agent.Services.Profiles;

internal interface IQueryProfileService : IQueryService<Profile, ProfileResource>
{
    Task<Profile> GetActiveAsync();
}