using Macone.Data;
using Macone.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Macone.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(User user) => await _db.Users.AddAsync(user);
        
        public async Task<bool> ExistsAsync(string username)
        {
            return await _db.Users.AnyAsync(u => u.Username == username);
        }

        public Task<User?> GetByCredentialsAsync(string username, string password)
        {
            return _db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
