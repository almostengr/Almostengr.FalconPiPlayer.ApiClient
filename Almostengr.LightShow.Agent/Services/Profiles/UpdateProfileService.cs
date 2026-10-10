using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.LightShow.Agent.Data;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;

namespace Almostengr.LightShow.Agent.Services.Profiles;

public sealed class UpdateProfileService : IUpdateService<ProfileResource>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper<Profile, ProfileResource> _mapper;
    private readonly IQueryService<Profile, ProfileResource> _queryService;

    public UpdateProfileService(
        ApplicationDbContext dbContext,
        IMapper<Profile, ProfileResource> mapper,
        IQueryService<Profile, ProfileResource> queryService
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _queryService = queryService;
    }

    public async Task<Result<ProfileResource>> ExecuteAsync(ProfileResource resource, bool commitTransaction = true)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            var entity = await _queryService.GetEntityByPublicIdAsync(resource.PublicId);
            if (entity == null)
            {
                return Result<ProfileResource>.Failure("Not found.");
            }

            var result = entity.Update(
                resource.Name,
                resource.TypeOption,
                resource.PlayerUrl,
                resource.WebsiteUrl,
                resource.WebsiteApiKey,
                resource.IsActive,
                resource.ModifiedBy,
                resource.WorkerSleepInterval);
            if (result.Failed)
            {
                return Result<ProfileResource>.Failure(result.Errors);
            }

            _dbContext.Profiles.Update(result.Value);
            await _dbContext.SaveChangesAsync();

            resource = _mapper.ToResource(result.Value);
            return Result<ProfileResource>.Success(resource);
        }
        catch (Exception ex)
        {
            return Result<ProfileResource>.Failure(ex.Message);
        }
    }
}
