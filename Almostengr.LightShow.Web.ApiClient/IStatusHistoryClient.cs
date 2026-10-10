using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.LightShow.Common;

namespace Almostengr.LightShow.Web.ApiClient;

public interface IStatusHistoryClient
{
    Task<Result<StatusHistoryResource>> CreateAsync(StatusHistoryResource resource);
}