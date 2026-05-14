namespace Ad_Backend.Application.DTOs;

public class VehicleDto
{
    public long Id { get; set; }

    
    public string VehicleNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Type { get; set; }
}
