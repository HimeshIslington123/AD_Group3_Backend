namespace Ad_Backend.Application.DTOs.PurchaseInvoices;

public class PurchaseInvoiceDto
{
    public long Id { get; set; }
    public long VendorId { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public List<PurchaseInvoiceItemDto> Items { get; set; } = new();
}
