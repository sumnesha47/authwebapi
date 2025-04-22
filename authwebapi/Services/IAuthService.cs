using authwebapi.Dtos;

namespace authwebapi.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
