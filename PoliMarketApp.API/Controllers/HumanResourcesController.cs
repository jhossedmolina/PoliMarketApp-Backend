using Microsoft.AspNetCore.Mvc;
using PoliMarketApp.Application.DTOs;
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

    [HttpPost("vendors")]
    public async Task<IActionResult> CreateVendor([FromBody] CreateVendedorDto vendorDto, CancellationToken cancellationToken)
    {
        var vendor = await _humanResourcesService.CreateVendorAsync(vendorDto, cancellationToken);
        if (vendor == null)
            return BadRequest(new { message = "Vendor with this document already exists" });

        return CreatedAtAction(nameof(GetVendor), new { vendorId = vendor.VendedorId }, vendor);
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

    [HttpGet("vendors")]
    public async Task<IActionResult> GetAllVendors(CancellationToken cancellationToken)
    {
        var vendors = await _humanResourcesService.GetAllVendorsAsync(cancellationToken);
        return Ok(vendors);
    }
}