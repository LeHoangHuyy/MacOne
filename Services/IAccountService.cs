using Macone.Models.Entities;

namespace Macone.Services
{
    public interface IAccountService
    {
        Task<User?> LoginAsync(string username, string password);
        Task<(bool success, string message)> RegisterAsync(User user);
    }
}
