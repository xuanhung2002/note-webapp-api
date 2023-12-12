using note_app_API.Database.Entities;

namespace note_app_API.DTOs
{
    public class CreateNoteDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public virtual List<CheckListItem> CheckListItems { get; set; } = new List<CheckListItem>();
    }
}
