using Microsoft.EntityFrameworkCore;
using PizzaMeow.Data;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;

namespace PizzaMeow.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private bool _disposed = false;
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("User data was not provided");
            else
            {
                await _context.Users.AddAsync(user);
            }
        }

        public async Task<User> GetUserAsync(int Id)
        {
            var userInDb = await _context.Users.FindAsync(new object[] { Id });
            if (userInDb == null) throw new ArgumentException("User not found");
            return userInDb;
        }

        public async Task<User> GetUserAsync(string email)
        {
            var userInDb = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (userInDb == null) throw new ArgumentNullException($"User with email {email} does not exist");
            else return userInDb;
        }

        public async Task<List<User>> GetUsersAsync() => await _context.Users.ToListAsync();

        public async Task UpdateUserAsync(User user, int Id)
        {
            var userInDb = await _context.Users.FindAsync(new object[] { Id });
            if (userInDb == null) throw new ArgumentNullException($"user with Id {Id} not found");
        }

        public async Task UpdateUserRoleAsync(int userId, int roleId)
        {
            var userInDb = await _context.Users.FindAsync(new object[] { userId });
            if (userInDb == null) throw new ArgumentException($"user with Id {userId} not found");
            if (roleId > 3 || roleId < 0) throw new ArgumentException($"role with Id {roleId} does not exist");
            userInDb.RoleId = roleId;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var userInDb = await _context.Users.FindAsync(new object[] { userId });
            if (userInDb == null) throw new ArgumentException($"user with Id {userId} not found");
            _context.Users.Remove(userInDb);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
