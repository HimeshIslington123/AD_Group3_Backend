using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class PurchaseInvoice
{
    public long Id { get; set; }
    public long VendorId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }

    public Vendor Vendor { get; set; }
    public ICollection<PurchaseInvoiceItem>? Items { get; set; }
}