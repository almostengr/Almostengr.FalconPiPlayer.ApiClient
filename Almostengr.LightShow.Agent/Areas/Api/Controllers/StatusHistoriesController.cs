using Almostengr.Common.Common.Shared;
using Almostengr.FalconPiPlayer.ApiClient.Fppd.DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Almostengr.LightShow.Agent.Areas.Api.Controllers;

public class StatusHistoriesController : ApiController
{
    private readonly IFppdClient _fppdClient;

    public StatusHistoriesController(
        IFppdClient fppdClient
    )
    {
        _fppdClient = fppdClient;
    }

    [HttpPost("offline")]
    public async Task<IActionResult> Offline()
    {
        return Ok();
    }

    [HttpPost("online")]
    public async Task<IActionResult> Online()
    {
        return Ok();
    }

    [HttpPost("disable")]
    public async Task<IActionResult> Disable()
    {
        return Ok();
    }

    [HttpPost("enable")]
    public async Task<IActionResult> Enable()
    {
        return Ok();
    }
}