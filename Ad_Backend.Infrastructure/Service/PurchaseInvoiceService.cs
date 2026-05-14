using Ad_Backend.Application.DTOs.PurchaseInvoices;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Service;

public class PurchaseInvoiceService : IPurchaseInvoiceService
{
    private readonly IPurchaseInvoiceRepository _repository;
    private readonly AppDbContext _context;

    public PurchaseInvoiceService(IPurchaseInvoiceRepository repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<PurchaseInvoiceDto> CreateInvoiceAsync(CreatePurchaseInvoiceDto dto)
    {
        var invoice = new PurchaseInvoice
        {
            VendorId = dto.VendorId,
            Date = DateTime.UtcNow,
            TotalAmount = 0,
            Items = new List<PurchaseInvoiceItem>()
        };

        foreach (var itemDto in dto.Items)
        {
            var part = await _context.Parts.FindAsync(itemDto.PartId);
            if (part == null)
            {
                throw new Exception($"Part with ID {itemDto.PartId} not found.");
            }

            if (part.VendorId != dto.VendorId)
            {
                throw new Exception($"Part '{part.Name}' does not belong to the selected vendor.");
            }

            var item = new PurchaseInvoiceItem
            {
                PartId = itemDto.PartId,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice
            };
            invoice.Items.Add(item);
            invoice.TotalAmount += itemDto.Quantity * itemDto.UnitPrice;

            // Update Stock
            part.StockQuantity += itemDto.Quantity;
        }

        var created = await _repository.CreateAsync(invoice);
        
        // Re-fetch to get Vendor and Part names if needed, 
        // or just map from what we have. For simplicity and correctness, 
        // let's fetch again or just return the mapped DTO.
        var fullInvoice = await _repository.GetByIdAsync(created.Id);
        return MapToDto(fullInvoice!);
    }

    public async Task<List<PurchaseInvoiceDto>> GetAllInvoicesAsync()
    {
        var invoices = await _repository.GetAllAsync();
        return invoices.Select(MapToDto).ToList();
    }

    public async Task<PurchaseInvoiceDto?> GetInvoiceByIdAsync(long id)
    {
        var invoice = await _repository.GetByIdAsync(id);
        return invoice == null ? null : MapToDto(invoice);
    }

    private PurchaseInvoiceDto MapToDto(PurchaseInvoice invoice)
    {
        return new PurchaseInvoiceDto
        {
            Id = invoice.Id,
            VendorId = invoice.VendorId,
            VendorName = invoice.Vendor?.Name ?? "Unknown",
            Date = invoice.Date,
            TotalAmount = invoice.TotalAmount,
            Items = invoice.Items?.Select(i => new PurchaseInvoiceItemDto
            {
                Id = i.Id,
                PartId = i.PartId,
                PartName = i.Part?.Name ?? "Unknown",
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList() ?? new List<PurchaseInvoiceItemDto>()
        };
    }
}
