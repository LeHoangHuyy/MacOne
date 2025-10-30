using Macone.Models.Entities;
using Macone.Repositories;

namespace Macone.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repo;
        public AccountService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            return await _repo.GetByCredentialsAsync(username, password);
        }

        public async Task<(bool success, string message)> RegisterAsync(User user)
        {
            if (await _repo.ExistsAsync(user.Username))
            {
                return (false, "Tên đăng nhập đã tồn tại!");
            }

            user.Role = "USER";
            
            await _repo.CreateAsync(user);
            await _repo.SaveChangesAsync();

            return (true, "Đăng ký thành công! Hãy đăng nhập");
        }
    }
}
