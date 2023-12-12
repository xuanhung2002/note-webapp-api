using note_app_API.Database.Entities;

namespace note_app_API.DTOs
{
    public class CheckListItemDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public bool IsChecked { get; set; }

    }
}
