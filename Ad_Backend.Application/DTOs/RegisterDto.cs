namespace Ad_Backend.Application.DTOs;

public class RegisterDto
{
    // USER
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "Customer"; // default
    
    public string? Position { get; set; }
}