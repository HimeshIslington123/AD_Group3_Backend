using System.Security.Claims;
using Ad_Backend.Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ad_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Ad_Backend.Application.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var result = await _service.CreateAppointmentAsync(dto, userId);

        return Ok(result);
    }
    

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] string status)
    {
        var result = await _service.UpdateStatusAsync(id, status);

        if (!result)
            return NotFound();

        return Ok();
    }
    
    [HttpGet("new")]
    public async Task<IActionResult> GetNew()
    {
        var data = await _service.GetNewAppointmentsAsync();
        return Ok(data);
    }

    [HttpPost("{id}/send-invoice")]
    public async Task<IActionResult> SendInvoice(long id)
    {
        await _service.SendInvoiceAsync(id);
        return Ok("Invoice sent");
    }
    
    [HttpPost("mark-notified")]
    public async Task<IActionResult> MarkNotified()
    {
        var data = await _service.GetNewAppointmentsAsync();

        await _service.MarkAppointmentsAsNotifiedAsync(data);

        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }
    
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyAppointments()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var data = await _service.GetByUserIdAsync(userId);

        return Ok(data);
    }
    
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(long customerId)
    {
        var data = await _service.GetByCustomerIdAsync(customerId);
        return Ok(data);
    }
}
