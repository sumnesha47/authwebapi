using authwebapi.Data;
using authwebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace authwebapi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email) =>
            await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                                 .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetUserByUsernameAsync(string username) =>
            await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                                 .FirstOrDefaultAsync(u => u.Username == username);

        public async Task AddUserAsync(User user) =>
            await _context.Users.AddAsync(user);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
