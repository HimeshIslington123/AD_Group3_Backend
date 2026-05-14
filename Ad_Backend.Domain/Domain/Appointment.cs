using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class Appointment
{
    public long Id { get; set; }

    public long CustomerId { get; set; }
    public long VehicleId { get; set; }

    public DateTime Date { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";

    public Customer Customer { get; set; }
    public Vehicle Vehicle { get; set; }
}