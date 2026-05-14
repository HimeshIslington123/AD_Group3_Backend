using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class Payment
{
    public long Id { get; set; }

    public long SalesInvoiceId { get; set; }

    public decimal AmountPaid { get; set; }
    public decimal RemainingAmount { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = "Pending";

    public SalesInvoice SalesInvoice { get; set; }
}