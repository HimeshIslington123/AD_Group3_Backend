using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;
using Microsoft.IdentityModel.Tokens;

namespace Ad_Backend.Infrastructure.Service;

public class AuthService: IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }


    private async Task<string> GenerateJwt(ApplicationUser user)
    {
        var roles = await _authRepository.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("THIS_IS_SUPER_SEffffffffffffCRET_KEY_12345")
        );

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

        var result = await _authRepository.RegisterAsync(user, dto.Password, dto.Role ?? "Staff");

        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));

        return "User registered successfully";
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _authRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("User not found");

        var valid = await _authRepository.CheckPasswordAsync(user, dto.Password);

        if (!valid)
            throw new Exception("Invalid credentials");

        var token = await GenerateJwt(user); 

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email
        };
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _authRepository.GetAllUsersAsync();
        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await _authRepository.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "No Role"
            });
        }

        return userDtos;
    }

    public async Task<string> UpdateUserRoleAsync(string userId, string newRole)
    {
        var users = await _authRepository.GetAllUsersAsync();
        var user = users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return "User not found";

        var result = await _authRepository.UpdateRoleAsync(user, newRole);
        return result.Succeeded ? "Role updated successfully" : string.Join(", ", result.Errors.Select(e => e.Description));
    }

    public async Task<string> DeleteUserAsync(string userId)
    {
        var result = await _authRepository.DeleteUserAsync(userId);
        return result.Succeeded ? "User deleted successfully" : string.Join(", ", result.Errors.Select(e => e.Description));
    }
}