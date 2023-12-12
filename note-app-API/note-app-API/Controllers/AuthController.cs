using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using note_app_API.Database.Entities;
using note_app_API.DTOs;
using note_app_API.Services.TokenService;
using note_app_API.Services.UserService;
using System.Security.Cryptography;
using System.Text;

namespace note_app_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {   
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDTO registerUser)
        {
            registerUser.Username = registerUser.Username.ToLower();
            if(await _userService.GetUserByUsername(registerUser.Username) is not null)
            {
                return BadRequest("Username is already existed!!!");
            }

            using var hashFunc = new HMACSHA256();
            var passwordByte = Encoding.UTF8.GetBytes(registerUser.Password);
            var newUser = new User
            {
                Username = registerUser.Username,
                Email = registerUser.Email,
                Name = registerUser.Name,
                PasswordHash = hashFunc.ComputeHash(passwordByte),
                PasswordSalt = hashFunc.Key
            };

            await _userService.CreateUser(newUser);
            return Ok(_tokenService.GenerateToken(newUser));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginDTO loginUser)
        {
            loginUser.Username = loginUser.Username.ToLower();
            var existedUser = await _userService.GetUserByUsername(loginUser.Username);
            if(existedUser is null) return Unauthorized("User name is not found");
            using var hashFunc = new HMACSHA256(existedUser.PasswordSalt);
            var passwordByte = Encoding.UTF8.GetBytes(loginUser.Password);
            var passwordHash = hashFunc.ComputeHash(passwordByte);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != existedUser.PasswordHash[i])
                {
                    return Unauthorized("Password not match");
                }
            }
            return Ok(_tokenService.GenerateToken(existedUser));
        }
    }
}
