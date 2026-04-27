using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class SalesInvoice
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsPaid { get; set; }

    public Customer Customer { get; set; }
    public ICollection<SalesInvoiceItem>? Items { get; set; }
    public Payment? Payment { get; set; }
}