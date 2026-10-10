using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.LightShow.Agent.Data;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;

namespace Almostengr.LightShow.Agent.Services.Profiles;

public sealed class AddProfileService : IAddService<ProfileResource>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper<Profile, ProfileResource> _mapper;

    public AddProfileService(
        ApplicationDbContext dbContext,
        IMapper<Profile, ProfileResource> mapper
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<ProfileResource>> ExecuteAsync(ProfileResource resource, bool commitTransaction = true)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(resource, nameof(resource));

            var result = Profile.Create(
                resource.Name,
                resource.TypeOption,
                resource.PlayerUrl,
                resource.WebsiteUrl,
                resource.WebsiteApiKey,
                resource.IsActive,
                resource.CreatedBy,
                resource.WorkerSleepInterval);
            if (result.Failed)
            {
                return Result<ProfileResource>.Failure(result.Errors);
            }

            await _dbContext.Profiles.AddAsync(result.Value);
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
