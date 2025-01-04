using PizzaMeow.Data;
using PizzaMeow.Data.Models;
using PizzaMeow.Data.Repos;

namespace PizzaMeow.Infrastructure.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository, IDisposable
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Role> GetRoleAsync(int Id)
        {
            var roleInDb = await _context.Roles.FindAsync(new object[] { Id });
            if (roleInDb == null)
            {
                throw new ArgumentException("Role with current Id does not exist");
            }
            else return roleInDb;
        }

        public async Task<Role> GetRoleAsync(string roleName)
        {
            var roleInDb = await _context.Roles.FindAsync(new object[] { roleName });
            if (roleInDb == null) throw new ArgumentException("Role with current name does not exist");
            else return roleInDb;
        }
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
