using Ad_Backend.Application.DTOs;

namespace Ad_Backend.Application.Interface.IService;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}