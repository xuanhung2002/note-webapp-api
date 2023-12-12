namespace note_app_API.Database.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime? Update_At { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual List<CheckListItem> CheckListItems { get; set; } = new List<CheckListItem>();
    }
}
