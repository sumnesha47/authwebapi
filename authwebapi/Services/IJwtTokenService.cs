using authwebapi.Models;

namespace authwebapi.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
