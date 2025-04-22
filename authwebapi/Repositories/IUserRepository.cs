using authwebapi.Models;

namespace authwebapi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<IList<string>> GetUserRolesAsync(User user);
    }
}
