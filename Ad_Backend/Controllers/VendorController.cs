using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.DTOs.Vendors;
using Ad_Backend.Application.Interface.IService;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VendorController : ControllerBase
{
    private readonly IVendorService _service;

    public VendorController(IVendorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVendorDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, CreateVendorDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(result);
    }
}