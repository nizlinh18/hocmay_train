using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week1.Rbac.Api.Contracts;
using Week1.Rbac.Api.Data;
using Week1.Rbac.Api.Models;

namespace Week1.Rbac.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PermissionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PermissionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PermissionResponse>>> GetAll()
    {
        var permissions = await _context.Permissions
            .OrderBy(x => x.Code)
            .Select(x => new PermissionResponse(
                x.Id,
                x.Code,
                x.Description))
            .ToListAsync();

        return Ok(permissions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PermissionResponse>> GetById(Guid id)
    {
        var permission = await _context.Permissions
            .Where(x => x.Id == id)
            .Select(x => new PermissionResponse(
                x.Id,
                x.Code,
                x.Description))
            .FirstOrDefaultAsync();

        if (permission is null)
            return NotFound();

        return Ok(permission);
    }

    [HttpPost]
    public async Task<ActionResult<PermissionResponse>> Create(
        CreatePermissionRequest request)
    {
        var code = request.Code.Trim().ToLowerInvariant();

        var exists = await _context.Permissions
            .AnyAsync(x => x.Code == code);

        if (exists)
            return Conflict("Permission code đã tồn tại.");

        var permission = new Permission
        {
            Code = code,
            Description = request.Description.Trim()
        };

        _context.Permissions.Add(permission);

        await _context.SaveChangesAsync();

        var response = new PermissionResponse(
            permission.Id,
            permission.Code,
            permission.Description
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = permission.Id },
            response
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdatePermissionRequest request)
    {
        var permission =
            await _context.Permissions.FindAsync(id);

        if (permission is null)
            return NotFound();

        var code = request.Code.Trim().ToLowerInvariant();

        var exists = await _context.Permissions
            .AnyAsync(x => x.Code == code && x.Id != id);

        if (exists)
            return Conflict("Permission code đã tồn tại.");

        permission.Code = code;
        permission.Description = request.Description.Trim();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var permission =
            await _context.Permissions.FindAsync(id);

        if (permission is null)
            return NotFound();

        _context.Permissions.Remove(permission);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}