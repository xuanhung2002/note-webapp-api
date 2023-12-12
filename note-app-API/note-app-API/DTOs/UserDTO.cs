using System.ComponentModel.DataAnnotations;

namespace note_app_API.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
