using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class PartRequest
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public string PartName { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime Date { get; set; }

    public Customer Customer { get; set; }
}