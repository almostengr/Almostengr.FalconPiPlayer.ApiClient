using Almostengr.Common.Common.DomainServices.Resources;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;

namespace Almostengr.LightShow.Agent.Services.Profiles;

public class ProfileResource : Resource
{
    public string Name { get; set; }
    public PlayerTypeOption TypeOption { get; set; }
    public string PlayerUrl { get; set; }
    public int WorkerSleepInterval { get; set; }
    public string WebsiteApiKey { get; set; }
    public string WebsiteUrl { get; set; }
    public bool IsActive { get; set; }
}