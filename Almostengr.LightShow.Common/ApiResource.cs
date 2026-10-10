using Almostengr.Common.Common.DomainServices.Resources;

namespace Almostengr.LightShow.Common;

public abstract class ApiResource : Resource
{
    public string ApiKey { get; set; }
}
