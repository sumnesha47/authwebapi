using authwebapi.Models;

namespace authwebapi.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetByNameAsync(string roleName);
        Task AddRoleAsync(Role role);
    }
}
