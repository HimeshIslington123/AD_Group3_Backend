using System.Security.Claims;
using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ad_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartRequestController : ControllerBase
{
    private readonly IRequestPartService _service;

    public PartRequestController(
        IRequestPartService service
    )
    {
        _service = service;
    }

    // CREATE REQUEST
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePartRequestDto dto
    )
    {
        var userId = User.FindFirst(
            ClaimTypes.NameIdentifier
        )?.Value;

        if (userId == null)
            return Unauthorized();

        var result = await _service.CreateAsync(
            dto,
            userId
        );

        return Ok(result);
    }

    // GET ALL
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();

        return Ok(data);
    }

    // GET MY REQUESTS
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyRequests()
    {
        var userId = User.FindFirst(
            ClaimTypes.NameIdentifier
        )?.Value;

        if (userId == null)
            return Unauthorized();

        var data = await _service
            .GetByUserIdAsync(userId);

        return Ok(data);
    }

    // GET SINGLE
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
            return NotFound();

        return Ok(data);
    }

    // UPDATE STATUS
    [Authorize]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        long id,
        [FromBody] UpdatePartRequestStatusDto dto
    )
    {
        var result = await _service
            .UpdateStatusAsync(id, dto.Status);

        if (!result)
            return NotFound();

        return Ok(new
        {
            message = "Status updated successfully"
        });
    }
}
