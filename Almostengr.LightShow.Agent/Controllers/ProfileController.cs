using Microsoft.AspNetCore.Mvc;
using Almostengr.LightShow.Agent.Models;
using Almostengr.Common.Common.Shared;
using Almostengr.LightShow.Agent.Services.Profiles.Domain;
using Almostengr.Common.Common.DomainServices.Interfaces;
using Almostengr.LightShow.Agent.Services.Profiles;

namespace Almostengr.LightShow.Agent.Controllers;

public class ProfileController : UiController
{
    private readonly IAddService<ProfileResource> _addService;
    private readonly IDeleteService<ProfileResource> _deleteService;
    private readonly IQueryService<Profile, ProfileResource> _queryService;
    private readonly IUpdateService<ProfileResource> _updateService;

    public ProfileController(
        IAddService<ProfileResource> addService,
        IDeleteService<ProfileResource> deleteService,
        IQueryService<Profile, ProfileResource> queryService,
        IUpdateService<ProfileResource> updateService
    )
    {
        _addService = addService;
        _deleteService = deleteService;
        _queryService = queryService;
        _updateService = updateService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var resource = await _queryService.GetListAsync();
        return View(resource);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ProfileViewModel model = new();
        return PartialView("_CreateEdit", model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var resource = await _queryService.GetByPublicIdAsync(id);
        if (resource == null)
        {
            return NotFoundParitalView();
        }

        ProfileViewModel model = new(resource);
        return PartialView("_CreateEdit", model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEdit(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_CreateEdit", model);
        }

        bool isNew = model.PublicId == Guid.Empty;

        ProfileResource resource = isNew ? new() : await _queryService.GetByPublicIdAsync(model.PublicId);
        if (resource == null)
        {
            return NotFoundParitalView();
        }

        model.AssignToResource(resource, User.Identity.Name);

        var result = isNew ?
            await _addService.ExecuteAsync(resource) : await _updateService.ExecuteAsync(resource);
        if (result.Succeeded)
        {
            return NoContent();
        }

        AddErrorsToModelState(result.Errors);
        return PartialView("_CreateEdit", model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var exists = await _queryService.ExistsByPublicIdAsync(id);
        if (!exists)
        {
            return NotFoundParitalView();
        }

        return PartialView("_Delete", id);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirm(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_Delete", id);
        }

        var resource = await _queryService.GetByPublicIdAsync(id);
        if (resource == null)
        {
            return NotFoundParitalView();
        }

        var result = await _deleteService.ExecuteAsync(resource);
        if (result.Succeeded)
        {
            return NoContent();
        }

        AddErrorsToModelState(result.Errors);
        return PartialView("_Delete", id);
    }
}
