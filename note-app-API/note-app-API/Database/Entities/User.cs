using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace note_app_API.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public virtual List<Note> Notes { get; set; } = new List<Note>();

    }
}
