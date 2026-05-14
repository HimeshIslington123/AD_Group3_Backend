namespace Ad_Backend.Application.DTOs;

public class RegisterDto
{
    // USER
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "Staff";

    // STAFF
    public string? Position { get; set; }

    // CUSTOMER
    public string? Phone { get; set; }
    public string? Address { get; set; }

    // VEHICLE
    public string? VehicleNumber { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? Type { get; set; }
}