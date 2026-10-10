namespace Almostengr.LightShow.Agent.Services.CurrentStatusManager.Domain;

public static class RunMode
{
    public static bool IsOnline { get; set; } = false;
    public static bool AllowRequests { get; set; } = false;
}
