using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using note_app_API.Database.Entities;
using note_app_API.DTOs;
using note_app_API.Services.NoteService;
using System.Security.Claims;

namespace note_app_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IMapper mapper;
        private readonly ILogger<NoteController> _logger;

        public NoteController(INoteService noteService, IMapper mapper, ILogger<NoteController> logger)
        {
            _noteService = noteService;
            this.mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("addNote")]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDTO createNoteDTO)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _logger.LogInformation(userId);
                if (int.TryParse(userId, out int id))
                {
                    var note = mapper.Map<Note>(createNoteDTO);
                    note.UserId = id;
                    await _noteService.CreateNote(note);
                    return StatusCode(StatusCodes.Status200OK, "Create success");
                }
                else
                {
                    return BadRequest("User id is not valid");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            };
        }

        [Authorize]
        [HttpGet("getNoteOfUser")]
        public async Task<ActionResult<List<NoteDTO>>> GetNotesOfUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _logger.LogInformation($"userId: {userId}");
                if (userId != null)
                {
                    var notes = await _noteService.getNotesOfUser(int.Parse(userId));
                    return Ok(notes);
                }
                else
                {
                    return Unauthorized("Please login!!");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
            
            
        }



    }
}
