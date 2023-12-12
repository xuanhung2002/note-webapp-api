using AutoMapper;
using Microsoft.EntityFrameworkCore;
using note_app_API.Database;
using note_app_API.Database.Entities;
using note_app_API.DTOs;
using note_app_API.Services.NoteService;

namespace note_app_API.Services.NoteService
{
    public class NoteService : INoteService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public NoteService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task CreateNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NoteDTO>?> getNotesOfUser(int userId)
        {
            var notes = await _context.Notes
                            .Where(u => u.UserId == userId)
                            .ToListAsync();

            if (notes != null && notes.Any())
            {
                var noteDTOs = notes.Select(n => _mapper.Map<NoteDTO>(n)).ToList();
                return noteDTOs;
            }
            return null;
        }
    }
}
