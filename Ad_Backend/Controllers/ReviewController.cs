using System.Security.Claims;
using Ad_Backend.Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ad_Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Ad_Backend.Application.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewController(
        IReviewService service
    )
    {
        _service = service;
    }

    // CREATE REVIEW
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateReviewDto dto
    )
    {
        var userId = User.FindFirst(
            ClaimTypes.NameIdentifier
        )?.Value;

        if (userId == null)
            return Unauthorized();

        var result = await _service
            .CreateAsync(dto, userId);

        return Ok(result);
    }

    // GET ALL REVIEWS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();

        return Ok(data);
    }
}
