using note_app_API.Database.Entities;
using note_app_API.DTOs;

namespace note_app_API.Services.NoteService
{
    public interface INoteService
    {
        Task CreateNote(Note note);
        Task<List<NoteDTO>?> getNotesOfUser(int userId);
    }
}
