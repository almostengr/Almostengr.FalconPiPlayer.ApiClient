using Almostengr.Common.Common.DomainServices;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.LightShow.Agent.Data;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.LightShow.Agent.Services.Profiles;

public sealed class QueryProfileService : QueryService<Profile, ProfileResource>, IQueryProfileService
{
    public QueryProfileService(
        ApplicationDbContext dbContext,
        IMapper<Profile, ProfileResource> mapper
    ) : base(dbContext, mapper)
    {
    }

    public async Task<Profile> GetActiveAsync()
    {
        var profile = await _dbSet.SingleOrDefaultAsync(p => p.IsActive);
        return profile;
    }
}
