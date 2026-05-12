using Ad_Backend.Application.DTOs;

namespace Ad_Backend.Application.Interface.IService;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<string> RegisterAsync(RegisterDto dto);
}