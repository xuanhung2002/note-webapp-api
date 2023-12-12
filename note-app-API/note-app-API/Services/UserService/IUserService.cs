using note_app_API.Database.Entities;

namespace note_app_API.Services.UserService
{
    public interface IUserService
    {
        Task<User?> GetUserByUsername(string username);
        Task CreateUser(User user);

    }
}
