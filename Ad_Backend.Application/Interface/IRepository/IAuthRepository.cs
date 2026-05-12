using Ad_Backend.Domain.Domain;
using Microsoft.AspNetCore.Identity;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

    Task AddToRoleAsync(ApplicationUser user, string role);
    Task<bool> RoleExistsAsync(string role);
    Task CreateRoleAsync(string role);

    Task CreateStaffAsync(Staff staff);
}