using Ad_Backend.Application.DTOs.PurchaseInvoices;
using Ad_Backend.Application.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ad_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseInvoiceController : ControllerBase
{
    private readonly IPurchaseInvoiceService _invoiceService;

    public PurchaseInvoiceController(IPurchaseInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await _invoiceService.GetAllInvoicesAsync();
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
        if (invoice == null) return NotFound();
        return Ok(invoice);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePurchaseInvoiceDto dto)
    {
        var created = await _invoiceService.CreateInvoiceAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
