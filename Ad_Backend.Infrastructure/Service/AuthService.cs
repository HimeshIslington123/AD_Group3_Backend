using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Service;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(IAuthRepository authRepository, UserManager<ApplicationUser> userManager)
    {
        _authRepository = authRepository;
        _userManager = userManager;
    }

    private string GenerateJwt(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_SUPER_SEffffffffffffCRET_KEY_12345"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "AdBackend",
            audience: "AdBackendUsers",
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName
        };

        var role = dto.Role ?? "Staff";
        var result = await _authRepository.RegisterAsync(user, dto.Password, role);

        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));

        if (!await _authRepository.RoleExistsAsync(role))
        {
            await _authRepository.CreateRoleAsync(role);
        }

        await _authRepository.AddToRoleAsync(user, role);

        if (role == "Staff")
        {
            var position = dto.Position ?? "Staff";
            var staff = new Staff
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserId = user.Id,
                Position = position,
            };
            await _authRepository.CreateStaffAsync(staff);
        }

        return $"{role} registered successfully";
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _authRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("User not found");

        var valid = await _authRepository.CheckPasswordAsync(user, dto.Password);

        if (!valid)
            throw new Exception("Invalid credentials");

        var token = GenerateJwt(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email
        };
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = new List<UserDto>();
        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            userDtos.Add(new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                Role = roles.FirstOrDefault() ?? "No Role"
            });
        }
        return userDtos;
    }

    public async Task<string> UpdateUserRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return "User not found";

        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRoleAsync(user, role);

        return "Role updated successfully";
    }

    public async Task<string> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return "User not found";

        await _userManager.DeleteAsync(user);
        return "User deleted successfully";
    }
}