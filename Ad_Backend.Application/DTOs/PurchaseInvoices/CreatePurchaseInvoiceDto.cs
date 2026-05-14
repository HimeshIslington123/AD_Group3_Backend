namespace Ad_Backend.Application.DTOs.PurchaseInvoices;

public class CreatePurchaseInvoiceDto
{
    public long VendorId { get; set; }
    public List<CreatePurchaseInvoiceItemDto> Items { get; set; } = new();
}
