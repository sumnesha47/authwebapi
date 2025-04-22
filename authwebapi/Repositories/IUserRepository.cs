using authwebapi.Models;

namespace authwebapi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}
