using Ad_Backend.Domain.Domain;
using Microsoft.AspNetCore.Identity;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterAsync(ApplicationUser user, string password, string role);
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
<<<<<<< HEAD
    Task<List<ApplicationUser>> GetAllUsersAsync();
    Task<IdentityResult> UpdateRoleAsync(ApplicationUser user, string newRole);
    Task<IdentityResult> DeleteUserAsync(string userId);
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
=======

    Task AddToRoleAsync(ApplicationUser user, string role);
    Task<bool> RoleExistsAsync(string role);
    Task CreateRoleAsync(string role);

    Task CreateStaffAsync(Staff staff);
>>>>>>> main
}