using System.ComponentModel.DataAnnotations;

namespace note_app_API.Database.Entities
{
    public class CheckListItem
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }
        public bool IsChecked { get; set; } = false;
        
        public int NoteId { get; set; }
        public virtual Note Note { get; set; } = null!;
    }
}
