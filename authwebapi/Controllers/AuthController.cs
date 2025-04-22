using authwebapi.Dtos;
using authwebapi.Models;
using authwebapi.Repositories;
using authwebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace authwebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (await _userRepository.GetUserByEmailAsync(request.Email) != null)
                return BadRequest("Email already in use.");

            if (await _userRepository.GetUserByUsernameAsync(request.Username) != null)
                return BadRequest("Username already taken.");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Username = request.Username,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _userRepository.AddUserAsync(user);

            if (await _userRepository.SaveChangesAsync())
                return Ok("User registered successfully.");

            return StatusCode(500, "Registration failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password.");

            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("This endpoint is accessible only to Admins.");
        }

        [Authorize]
        [HttpGet("user-profile")]
        public IActionResult GetUserProfile()
        {
            return Ok($"Hello {User.Identity?.Name}, your profile is secure.");
        }
    }
        
}
