using Microsoft.AspNetCore.Mvc;
using Almostengr.Common.Common.Shared;
using Almostengr.LightShow.Agent.Services.AppSettingsManager.Domain;
using Microsoft.Extensions.Options;
using Almostengr.LightShow.Agent.Services.AppSettingsManager;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.LightShow.Agent.Models;

namespace Almostengr.LightShow.Agent.Controllers;

public class AppSettingController : UiController
{
    private readonly AppSettings _appSettings;
    private readonly IUpdateService<AppSettingsResource> _updateService;

    public AppSettingController(
        IOptionsSnapshot<AppSettings> options,
        IUpdateService<AppSettingsResource> updateService
    )
    {
        _appSettings = options.Value;
        _updateService = updateService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        AppSettings model = _appSettings;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        AppSettings model = _appSettings;
        return PartialView("_Edit", model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AppSettingsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_Edit", model);
        }

        AppSettingsResource resource = new();
        model.AssignToResource(resource);

        var result = await _updateService.ExecuteAsync(resource);
        if (result.Succeeded)
        {
            return NoContent();
        }

        AddErrorsToModelState(result.Errors);
        return PartialView("_Edit", model);
    }
}