using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.LightShow.Agent.Data;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.LightShow.Agent.Services.Profiles;

public sealed class DeleteProfileService : IDeleteService<ProfileResource>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IQueryService<Profile, ProfileResource> _queryService;

    public DeleteProfileService(
        ApplicationDbContext dbContext,
        IQueryService<Profile, ProfileResource> queryService
    )
    {
        _dbContext = dbContext;
        _queryService = queryService;
    }

    public async Task<Result<ProfileResource>> ExecuteAsync(ProfileResource resource, bool commitTransaction = true)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            var profile = await _queryService.GetEntityByPublicIdAsync(resource.PublicId);
            if (profile == null)
            {
                return Result<ProfileResource>.Failure("Not found.");
            }

            if (profile.IsActive)
            {
                return Result<ProfileResource>.Failure("Cannot delete an active profile.");
            }

            await _dbContext.Profiles
                .Where(p => p.PublicId == resource.PublicId)
                .ExecuteDeleteAsync();

            return Result<ProfileResource>.Success(null);
        }
        catch (Exception ex)
        {
            return Result<ProfileResource>.Failure(ex.Message);
        }
    }
}