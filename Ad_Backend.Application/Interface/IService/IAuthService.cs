using Ad_Backend.Application.DTOs;

namespace Ad_Backend.Application.Interface.IService;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<string> RegisterAsync(RegisterDto dto);
<<<<<<< HEAD
    Task<List<UserDto>> GetAllUsersAsync();
    Task<string> UpdateUserRoleAsync(string userId, string newRole);
=======
    
    // Management
    Task<List<UserDto>> GetAllUsersAsync();
    Task<string> UpdateUserRoleAsync(string userId, string role);
>>>>>>> main
    Task<string> DeleteUserAsync(string userId);
}