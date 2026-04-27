using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class Notification
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRead { get; set; }

    public DateTime Date { get; set; }

    public ApplicationUser User { get; set; }
}