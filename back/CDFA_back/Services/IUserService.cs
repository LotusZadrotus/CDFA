using CDFA_back.DTO;

namespace CDFA_back.Services
{
    public interface IUserService
    {
        public Task<TokenDTO> GetTokenAsync(string name, string password);
        public Task<UserDTO> GetUserAsync(string name);
        public Task<UserDTO> RegistrateUser(RegistrateUserDTO user);
    }
}
