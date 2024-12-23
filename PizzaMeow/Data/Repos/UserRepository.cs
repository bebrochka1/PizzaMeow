using Microsoft.EntityFrameworkCore;
using PizzaMeow.Data.Models;

namespace PizzaMeow.Data.Repos
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private bool _disposed = false;
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(UserRegisterDTO userDTO)
        {
            if(userDTO == null) throw new ArgumentNullException("User data was not provided");
            else
            {
                User user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    PhoneNumber = userDTO.PhoneNumber,
                    PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(userDTO.Password, BCrypt.Net.HashType.SHA256),
                    RoleId = 2,
                };

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
            var userInDb = await _context.Users.FindAsync(new object[] {Id});
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
