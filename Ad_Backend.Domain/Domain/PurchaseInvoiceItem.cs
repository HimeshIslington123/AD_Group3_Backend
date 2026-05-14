using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class PurchaseInvoiceItem
{
    public long Id { get; set; }

    public long PurchaseInvoiceId { get; set; }
    public long PartId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public PurchaseInvoice PurchaseInvoice { get; set; }
    public Part Part { get; set; }
}