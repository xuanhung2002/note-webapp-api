using note_app_API.Database.Entities;

namespace note_app_API.Services.TokenService
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
