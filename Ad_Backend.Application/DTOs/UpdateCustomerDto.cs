namespace Ad_Backend.Application.DTOs;

public class UpdateCustomerDto
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public List<UpdateVehicleDto> Vehicles { get; set; } = new();
}
