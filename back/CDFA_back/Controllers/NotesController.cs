using CDFA_back.DTO;
using CDFA_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDFA_back.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NotesController: ControllerBase
    {
        private readonly INoteService _notes;
        private readonly ILogger<NotesController> _logger;
        public NotesController(INoteService notes, ILogger<NotesController> logger)
        {
            _notes = notes;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]/{page:int}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotes(int page, int? count)
        {
            try
            {
                if(count is null)
                {
                    count = 10;
                }
                var user_name = this.User.Claims.FirstOrDefault()?.Value;
                if(user_name is not null)
                {
                    var toReturn = await _notes.GetNotesAsync(page, count.Value, user_name);
                    return new OkObjectResult(toReturn);
                }return new StatusCodeResult(500);
            }
            catch(InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method GetNotes in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Authorize]
        [Route("[action]/{page:int}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetArchivedNotes(int page, int? count)
        {
            try
            {
                if (count is null)
                {
                    count = 10;
                }
                var user_name = this.User.Claims.FirstOrDefault()?.Value;
                if (user_name is not null)
                {
                    var toReturn = await _notes.GetArchivedNotesAsync(page, count.Value, user_name);
                    return new OkObjectResult(toReturn);
                }
                return new StatusCodeResult(500);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method GetArchivedNotes in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<NoteDTO>> CreateNote([FromBody]CreateNoteDTO note)
        {
            try
            {
                var user_name = this.User.Claims.FirstOrDefault()?.Value;
                if (user_name is not null)
                {
                    var toReturn = await _notes.CreateNoteAsync(note, user_name);
                    return new OkObjectResult(toReturn);
                }
                return new StatusCodeResult(500);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method CreateNote in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }
        [HttpPatch]
        [Authorize]
        [Route("[action]/{id:int}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> CompleteNote(int id)
        {
            try
            {
                var user_name = this.User.Claims.FirstOrDefault()?.Value;
                if (user_name is not null)
                {
                    var toReturn = await _notes.CompleteNoteAsync(id, user_name);
                    return new OkObjectResult(toReturn);
                }
                return new StatusCodeResult(500);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method CompleteNote in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }
        [HttpPatch]
        [Authorize]
        [Route("[action]/{id:int}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> ArchiveNote(int id)
        {
            try
            {
                var user_name = this.User.Claims.FirstOrDefault()?.Value;
                if (user_name is not null)
                {
                    var toReturn = await _notes.ArchiveNoteAsync(id, user_name);
                    return new OkObjectResult(toReturn);
                }
                return new StatusCodeResult(500);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method ArchiveNote in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }
    }
}
