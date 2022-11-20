using CDFA_back.Models;

namespace CDFA_back.DAL
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsync(int id);
        public Task<User> GetUserByNameAsync(string name);
        public Task<User> RegistrateUserAsync(User user);
    }
}
