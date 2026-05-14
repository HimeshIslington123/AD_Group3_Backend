namespace Ad_Backend.Application.DTOs.PurchaseInvoices;

public class PurchaseInvoiceItemDto
{
    public long Id { get; set; }
    public long PartId { get; set; }
    public string PartName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
