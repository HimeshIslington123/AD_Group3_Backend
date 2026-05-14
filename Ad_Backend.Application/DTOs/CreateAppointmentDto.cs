namespace Ad_Backend.Application.DTOs;

public class CreateAppointmentDto
{
 
    public long VehicleId { get; set; }

    public DateTime Date { get; set; }
    public string ServiceType { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? SearchParts { get; set; }
}
