using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class SalesInvoiceItem
{
    public long Id { get; set; }

    public long SalesInvoiceId { get; set; }
    public long PartId { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public SalesInvoice SalesInvoice { get; set; }
    public Part Part { get; set; }
}