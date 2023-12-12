using AutoMapper;
using note_app_API.Database.Entities;
using note_app_API.DTOs;

namespace note_app_API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateNoteDTO, Note>();

            CreateMap<Note, NoteDTO>();

            CreateMap<CheckListItem, CheckListItemDTO>();

            CreateMap<User, UserDTO>();
        }
    }
}
