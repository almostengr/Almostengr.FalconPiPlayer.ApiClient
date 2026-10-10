using Almostengr.LightShow.Agent.Services.Profiles;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;

namespace Almostengr.LightShow.Agent.Models;

public class ProfileViewModel
{
    public ProfileViewModel()
    {
    }

    public ProfileViewModel(ProfileResource resource)
    {
        Name = resource.Name;
        TypeOption = resource.TypeOption;
        PlayerUrl = resource.PlayerUrl;
        WebsiteUrl = resource.WebsiteUrl;
        WebsiteApiKey = resource.WebsiteApiKey;
        IsActive = resource.IsActive;
        PublicId = resource.PublicId;
        WorkSleepInterval = resource.WorkerSleepInterval;
    }

    public string Name { get; set; }
    public PlayerTypeOption TypeOption { get; set; }
    public string PlayerUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public string WebsiteApiKey { get; set; }
    public bool IsActive { get; set; }
    public Guid PublicId { get; set; }
    public int WorkSleepInterval { get; set; }

    internal void AssignToResource(ProfileResource resource, string modifiedBy)
    {
        if (resource == null)
        {
            return;
        }

        resource.Name = Name;
        resource.TypeOption = TypeOption;
        resource.PlayerUrl = PlayerUrl;
        resource.WebsiteUrl = WebsiteUrl;
        resource.WebsiteApiKey = WebsiteApiKey;
        resource.IsActive = IsActive;
        resource.PublicId = PublicId;
        resource.ModifiedBy = modifiedBy;
    }
}
