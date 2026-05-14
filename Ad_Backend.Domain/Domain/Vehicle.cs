using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class Vehicle
{
    public long Id { get; set; }
    public long CustomerId { get; set; }

    public string VehicleNumber { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Type { get; set; } = string.Empty;

    public Customer Customer { get; set; }
}