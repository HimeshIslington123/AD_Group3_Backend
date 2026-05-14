using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class Review
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public Customer Customer { get; set; }
}