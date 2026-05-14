using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class Part
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public long VendorId { get; set; }
    public Vendor Vendor { get; set; }
}