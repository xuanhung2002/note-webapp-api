using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using note_app_API.DTOs;
using note_app_API.Services.UserService;
using System.Security.Claims;

namespace note_app_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("getUserInfomation")]
        public async Task<ActionResult<UserDTO>> getUserInfomation()
        {
            var username = User.FindFirst("username")?.Value;
            if(username == null)
            {
                return Unauthorized("Please login!~");
            }
            
            var userInfo =  _mapper.Map<UserDTO>(await _userService.GetUserByUsername(username));
            return Ok(userInfo);
        }
    }
}
