using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class Customer
{
    public long Id { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // FK (FIXED)
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public ICollection<Vehicle>? Vehicles { get; set; }
    public ICollection<SalesInvoice>? SalesInvoices { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
}