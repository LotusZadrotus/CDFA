using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDFA_back.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        [Required]
        [StringLength(256)]
        public string Hashed { get; set; }
        [Required]
        [StringLength(128)]
        public string Salt { get; set; }
        public User(string name, string hashed, string salt)
        {
            Name = name;
            Hashed = hashed;
            Salt = salt;
        }
    }
}
