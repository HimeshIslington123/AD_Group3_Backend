namespace Ad_Backend.Application.DTOs;

public class CreatePartRequestDto
{
    public long VehicleId { get; set; }

    public string PartName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string Urgency { get; set; } = "Normal";

    public string? Notes { get; set; }
}
