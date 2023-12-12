using AutoMapper;
using Microsoft.EntityFrameworkCore;
using note_app_API.Database;
using note_app_API.Database.Entities;

namespace note_app_API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
             _context.Users.Add(user);
             await _context.SaveChangesAsync();
            
        }

        public async Task<User?> GetUserByUsername(string username)
        {   
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if(user == null)
            {
                return null;
            }
            return user;
        }
    }
}
