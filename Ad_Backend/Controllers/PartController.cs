using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ad_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Only Admin can manage parts
public class PartController : ControllerBase
{
    private readonly IPartService _partService;

    public PartController(IPartService partService)
    {
        _partService = partService;
    }

    [HttpGet]
    [AllowAnonymous] // Allow anyone to see parts? User said "Admin can perform parts management", maybe viewing is public.
    public async Task<IActionResult> GetAll()
    {
        var parts = await _partService.GetAllPartsAsync();
        return Ok(parts);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(long id)
    {
        var part = await _partService.GetPartByIdAsync(id);
        if (part == null) return NotFound();
        return Ok(part);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Part part)
    {
        var created = await _partService.CreatePartAsync(part);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Part part)
    {
        var updated = await _partService.UpdatePartAsync(id, part);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _partService.DeletePartAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPost("{id}/purchase")]
    public async Task<IActionResult> Purchase(long id, [FromBody] int quantity)
    {
        var part = await _partService.GetPartByIdAsync(id);
        if (part == null) return NotFound();

        part.StockQuantity += quantity;
        await _partService.UpdatePartAsync(id, part);
        
        return Ok(new { message = "Purchased successfully", newStock = part.StockQuantity });
    }
}