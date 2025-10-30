using Macone.Models.Entities;

namespace Macone.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByCredentialsAsync(string username, string password);
        Task<bool> ExistsAsync(string username);
        Task CreateAsync(User user);
        Task SaveChangesAsync();
    }
}
