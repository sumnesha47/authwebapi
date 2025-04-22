using authwebapi.Dtos;
using authwebapi.Models;
using authwebapi.Repositories;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace authwebapi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IRoleRepository roleRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _config = config;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepo.GetByUsernameAsync(dto.Username) != null)
                return false;

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Assign default role
            var defaultRoleName = "User";
            var role = await _roleRepo.GetByNameAsync(defaultRoleName);
            if (role == null)
            {
                role = new Role { Name = defaultRoleName };
                await _roleRepo.AddRoleAsync(role);
            }

            user.UserRoles = new List<UserRole>
        {
            new UserRole { User = user, Role = role }
        };

            await _userRepo.AddUserAsync(user);
            return true;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo
    ::contentReference[oaicite: 14]{ index = 14}
        }

}
