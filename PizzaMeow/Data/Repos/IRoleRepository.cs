using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleAsync(int Id);
        Task<Role> GetRoleAsync(string roleName);
    }
}
