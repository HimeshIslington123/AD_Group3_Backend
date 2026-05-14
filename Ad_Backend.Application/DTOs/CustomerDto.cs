namespace Ad_Backend.Application.DTOs;

public class CustomerDto
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    
    // CUSTOMER
    public string Phone { get; set; }
    public string Address { get; set; }
    public List<VehicleDto> Vehicles { get; set; }
    public List<SalesInvoiceDto> SalesInvoices { get; set; }
    public List<AppointmentDto> Appointments { get; set; }
}
