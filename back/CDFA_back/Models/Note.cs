using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDFA_back.Models
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("is_completed")]
        public bool IsCompleted { get; set; }

        [Required]
        [Column("is_archived")]
        public bool IsArchived { get; set; }
        [Required]
        [Column("title")]
        [StringLength(64)]
        public string Title { get; set; }
        [Required]
        [Column("desc")]
        public string Description { get; set; }
        [Column("user_id")]
        [Required]
        public int Owner { get; set; }
        [ForeignKey("Owner")]
        public User? User { get; set; }
        public Note()
        {

        }
        public Note(bool isCompleted, string title, string description)
        {
            IsCompleted = isCompleted;
            Title = title;
            Description = description;
        }
        public Note(string title, string description, int owner)
        {
            IsCompleted=false;
            IsArchived=false;
            Title = title;
            Description = description;
            Owner = owner;
        }
    }
}
