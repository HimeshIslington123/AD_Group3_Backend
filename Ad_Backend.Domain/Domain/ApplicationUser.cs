using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}