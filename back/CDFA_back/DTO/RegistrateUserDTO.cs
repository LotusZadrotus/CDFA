using CDFA_back.Models;

namespace CDFA_back.DTO
{
    public record RegistrateUserDTO: UserDTO
    {
        public string Password { get; init; }
        public RegistrateUserDTO(string name, string password)
            :base(name)
        {
            Password = password;
        }
    }
}
