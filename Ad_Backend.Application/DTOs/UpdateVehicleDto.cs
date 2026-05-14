namespace Ad_Backend.Application.DTOs;

public class UpdateVehicleDto
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
}
