using Microsoft.AspNetCore.Mvc;

namespace Ad_Backend.Controllers;

using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAllStaff()
    {
        var result = await _staffService.GetAllStaffAsync();
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetStaffById(int id)
    {
        var staff = await _staffService.GetStaffByIdAsync(id);

        if (staff == null)
            return NotFound(new { message = "Staff not found" });

        return Ok(staff);
    }

  
    [HttpPost]
    public async Task<IActionResult> CreateStaff([FromBody] Staff staff)
    {
        var created = await _staffService.CreateStaffAsync(staff);
        return Ok(created);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStaff(int id, [FromBody] Staff staff)
    {
        var updated = await _staffService.UpdateStaffAsync(id, staff);

        if (updated == null)
            return NotFound(new { message = "Staff not found" });

        return Ok(updated);
    }

  
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStaff(int id)
    {
        var deleted = await _staffService.DeleteStaffAsync(id);

        if (!deleted)
            return NotFound(new { message = "Staff not found" });

        return Ok(new { message = "Staff deleted successfully" });
    }
}