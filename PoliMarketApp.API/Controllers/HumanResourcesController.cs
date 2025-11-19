using Microsoft.AspNetCore.Mvc;
using PoliMarketApp.Application.Interfaces;

namespace PoliMarketApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HumanResourcesController : ControllerBase
{
    private readonly IHumanResourcesService _humanResourcesService;

    public HumanResourcesController(IHumanResourcesService humanResourcesService)
    {
        _humanResourcesService = humanResourcesService;
    }

    [HttpPost("{vendorId}/authorize")]
    public async Task<IActionResult> AuthorizeVendor(int vendorId, CancellationToken cancellationToken)
    {
        var result = await _humanResourcesService.AuthorizeVendorAsync(vendorId, cancellationToken);
        if (!result)
            return NotFound(new { message = "Vendor not found" });

        return Ok(new { message = "Vendor authorized successfully" });
    }

    [HttpPost("{vendorId}/revoke-authorization")]
    public async Task<IActionResult> RevokeVendorAuthorization(int vendorId, CancellationToken cancellationToken)
    {
        var result = await _humanResourcesService.RevokeVendorAuthorizationAsync(vendorId, cancellationToken);
        if (!result)
            return NotFound(new { message = "Vendor not found" });

        return Ok(new { message = "Vendor authorization revoked successfully" });
    }

    [HttpGet("authorized-vendors")]
    public async Task<IActionResult> GetAuthorizedVendors(CancellationToken cancellationToken)
    {
        var vendors = await _humanResourcesService.GetAuthorizedVendorsAsync(cancellationToken);
        return Ok(vendors);
    }

    [HttpGet("vendors/{vendorId}")]
    public async Task<IActionResult> GetVendor(int vendorId, CancellationToken cancellationToken)
    {
        var vendor = await _humanResourcesService.GetVendorByIdAsync(vendorId, cancellationToken);
        if (vendor == null)
            return NotFound();

        return Ok(vendor);
    }
}