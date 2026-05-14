using Microsoft.AspNetCore.Identity;
namespace Ad_Backend.Domain.Domain;

public class AIRecommendation
{
    public long Id { get; set; }

    public long VehicleId { get; set; }

    public string PartName { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public Vehicle Vehicle { get; set; }
}