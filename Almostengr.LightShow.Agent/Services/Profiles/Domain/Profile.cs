using System.ComponentModel.DataAnnotations;
using Almostengr.Common.Common.Domain;
using Almostengr.Common.Common.DomainServices.Results;

namespace Almostengr.LightShow.Agent.Services.Profiles.Domain;

public class Profile : Entity
{
    public Profile(string createdBy) : base(Guid.Empty, createdBy)
    {
    }

    private Profile()
    {
    }

    [Required]
    public string Name { get; private set; }
    
    public int PlayerTypeId { get; private set; }
    
    public string PlayerUrl { get; private set; } = "http://localhost";
    
    public string WebsiteUrl { get; private set; } = "http://lightshow.com";
    
    public string WebsiteApiKey { get; private set; }
    
    public bool IsActive { get; private set; }
    public PlayerTypeOption TypeOption => (PlayerTypeOption)PlayerTypeId;

    [Range(1, int.MaxValue, ErrorMessage = "Sleep interval must be greater than zero.")]
    public int WorkerSleepInterval { get; private set; }

    public static Result<Profile> Create(
        string name, PlayerTypeOption typeOption, string playerUrl, string websiteUrl, string websiteApiKey, bool isActive, string createdBy, int workerSleepInterval)
    {
        Profile profile = new(createdBy);
        return profile.Update(name, typeOption, playerUrl, websiteUrl, websiteApiKey, isActive, createdBy, workerSleepInterval);
    }

    internal static object Create(string name, PlayerTypeOption typeOption, string playerUrl, object websiteUrl, object websiteApiKey, object isActive, string createdBy, object workerSleepInterval)
    {
        throw new NotImplementedException();
    }

    internal static object Create(string name, PlayerTypeOption typeOption, string playerUrl, object websiteUrl, object websiteApiKey, object isActive, string createdBy, int workerSleepInterval)
    {
        throw new NotImplementedException();
    }

    internal Result<Profile> Update(
        string name, PlayerTypeOption playerType, string playerUrl, string websiteUrl, string websiteApiKey, bool isActive, string modifiedBy, 
        int workerSleepInterval)
    {
        Name = name;
        PlayerTypeId = (int)playerType;
        PlayerUrl = playerUrl;
        WebsiteUrl = websiteUrl;
        WebsiteApiKey = websiteApiKey;
        IsActive = isActive;
        WorkerSleepInterval = workerSleepInterval;
        SetModified(modifiedBy);
        return Result<Profile>.Success(this);
    }
}
