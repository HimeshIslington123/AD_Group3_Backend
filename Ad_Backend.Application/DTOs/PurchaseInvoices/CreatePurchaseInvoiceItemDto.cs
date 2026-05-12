namespace Ad_Backend.Application.DTOs.PurchaseInvoices;

public class CreatePurchaseInvoiceItemDto
{
    public long PartId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
