using CDFA_back.Models;

namespace CDFA_back.DTO
{
    public record UserDTO
    {
        public string Name { get;init;}
        public UserDTO(User user)
        {
            Name = user.Name;
        }
        public UserDTO(string name) { Name = name; }
    }
}
