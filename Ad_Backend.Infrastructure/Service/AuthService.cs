using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;
using Microsoft.IdentityModel.Tokens;

namespace Ad_Backend.Infrastructure.Service;

<<<<<<< HEAD
public class AuthService : IAuthService
=======
public class AuthService: IAuthService
>>>>>>> origin/main
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
<<<<<<< HEAD
=======


>>>>>>> origin/main
    private string GenerateJwt(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(
<<<<<<< HEAD
            Encoding.UTF8.GetBytes("THIS_IS_SUPER_SECREddddddededededT_KEY_12345")
=======
            Encoding.UTF8.GetBytes("THIS_IS_SUPER_SEffffffffffffCRET_KEY_12345")
>>>>>>> origin/main
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
<<<<<<< HEAD

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
  
=======
    
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
>>>>>>> origin/main
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName
        };

        var result = await _authRepository.RegisterAsync(user, dto.Password);

        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));

<<<<<<< HEAD
        var role = dto.Role ?? "Staff";


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
=======
        return "User registered successfully";
>>>>>>> origin/main
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _authRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("User not found");

        var valid = await _authRepository.CheckPasswordAsync(user, dto.Password);

        if (!valid)
            throw new Exception("Invalid credentials");

<<<<<<< HEAD
        var token = GenerateJwt(user);
=======
        var token = GenerateJwt(user); 
>>>>>>> origin/main

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email
        };
    }
}