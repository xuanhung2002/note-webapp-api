using Microsoft.EntityFrameworkCore;
using note_app_API.Database.Entities;

namespace note_app_API.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<CheckListItem> CheckList { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasOne<User>(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(s => s.UserId);
            });

            modelBuilder.Entity<CheckListItem>(entity =>
            {
                entity.HasOne<Note>(c => c.Note)
                .WithMany(n => n.CheckListItems)
                .HasForeignKey(s => s.NoteId);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
