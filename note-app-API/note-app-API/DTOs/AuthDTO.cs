using System.ComponentModel.DataAnnotations;

namespace note_app_API.DTOs
{
    public class AuthRegisterDTO
    {
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        [MaxLength(100)]
        public string Password { get; set; } = null!;
        [MaxLength (255)]
        public string Name { get; set; } = null!;
    }
    public class AuthLoginDTO 
    {
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        [MaxLength(100)]
        public string Password { get; set; } = null!;
    }
}
