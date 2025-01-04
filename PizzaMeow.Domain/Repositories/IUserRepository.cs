using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(int Id);
        Task<User> GetUserAsync(string email);
        Task<List<User>> GetUsersAsync();
        Task UpdateUserAsync(User user, int Id);
        Task UpdateUserRoleAsync(int userId, int roleId);
        Task DeleteUserAsync(int Id);
        Task SaveAsync();
    }
}
