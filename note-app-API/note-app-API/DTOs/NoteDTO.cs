using note_app_API.Database.Entities;

namespace note_app_API.DTOs
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime? Update_At { get; set; }
        public virtual List<CheckListItemDTO> CheckListItems { get; set; } = new List<CheckListItemDTO>();
    }
}
